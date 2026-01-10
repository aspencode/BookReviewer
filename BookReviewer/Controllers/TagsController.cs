using BookReviewer.Data;
using BookReviewer.Models.DTOs.Books;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetTags()
    {
        return await _context.Tags
            .Select(t => new TagDto
            {
                Id = t.Id,
                Name = t.Name
            })
            .ToListAsync();
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<TagDto>> CreateTag(CreateTagDto dto)
    {
        if (await _context.Tags.AnyAsync(t => t.Name == dto.Name))
            return BadRequest("Tag already exists");

        var tag = new Tag { Name = dto.Name };
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        return new TagDto
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
