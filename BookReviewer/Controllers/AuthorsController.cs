using BookReviewer.Data;
using BookReviewer.Models.DTOs.Authors;
using BookReviewer.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookReviewer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorListDto>>> GetAuthors()
        {
            var authors = await _context.Authors
                .Select(a => new AuthorListDto
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToListAsync();

            return Ok(authors);
        }

        // GET: api/authors/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDetailsDto>> GetAuthor(int id)
        {
            var author = await _context.Authors
                .Where(a => a.Id == id)
                .Select(a => new AuthorDetailsDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    BookCount = a.Books.Count
                })
                .FirstOrDefaultAsync();

            if (author == null)
                return NotFound();

            return Ok(author);
        }

        // POST: api/authors
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<AuthorDetailsDto>> CreateAuthor(
            CreateAuthorDto dto)
        {
            var author = new Author
            {
                Name = dto.Name
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAuthor),
                new { id = author.Id },
                new AuthorDetailsDto
                {
                    Id = author.Id,
                    Name = author.Name,
                    BookCount = 0
                }
            );
        }
    }
}
