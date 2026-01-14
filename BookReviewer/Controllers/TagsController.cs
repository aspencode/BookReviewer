using BookReviewer.Data;
using BookReviewer.Models.DTOs.Books;
using BookReviewer.Models.DTOs.Common;
using BookReviewer.Models.DTOs.Tags;
using BookReviewer.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/Tags")]
public class TagsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TagsController(ApplicationDbContext context)
    {
        _context = context;
    }


    // GET: api/tags?pageNumber=1&pageSize=10

    [HttpGet]
    public async Task<ActionResult<PagedResult<TagListDto>>> GetTags(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = _context.Tags.AsQueryable();
        var totalCount = await query.CountAsync();
        var tags = await query
            .OrderBy(t=>t.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(t=>new TagListDto
            {
                Id = t.Id,
                Name = t.Name
            })
            .ToListAsync();
        var result = new PagedResult<TagListDto>
        {
            Items = tags,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<TagListDto>> CreateTag(CreateTagDto dto)
    {
        if (await _context.Tags.AnyAsync(t => t.Name == dto.Name))
            return BadRequest("Tag already exists");

        var tag = new Tag { Name = dto.Name };
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        return new TagListDto
        {
            Id = tag.Id,
            Name = tag.Name
        };
    }

    [HttpGet("{tagId}/books")]
    public async Task<ActionResult<IEnumerable<BookListDto>>> GetBooksByTag(int tagId)
    {
        var tagExists = await _context.Tags.AnyAsync(t => t.Id == tagId);
        if (!tagExists)
            return NotFound("Tag not found.");

        var books = await _context.Books
            .Where(b => b.Tags.Any(t => t.Id == tagId))
            .Select(b => new BookListDto
            {
                Id = b.Id,
                ISBN = b.ISBN,
                Title = b.Title,
                AverageRating = _context.CalculateAverageRating(b.Id),
                Authors = b.Authors.Select(a => a.Name).ToList()
            })
            .ToListAsync();

        return Ok(books);
    }

}
