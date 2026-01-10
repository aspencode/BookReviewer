using BookReviewer.Data;
using BookReviewer.Models.DTOs.Books;
using BookReviewer.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookReviewer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookListDto>>> GetBooks()
        {
            var books = await _context.Books
                .Select(b => new BookListDto
                {
                    Id = b.Id,
                    ISBN = b.ISBN,
                    Title = b.Title,
                    Authors = b.Authors.Select(a => a.Name).ToList(),
                    AverageRating = _context.CalculateAverageRating(b.Id),
                })
                .ToListAsync();

            return Ok(books);
        }

        // GET: api/books/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
        {
            var book = await _context.Books
                .Where(b => b.Id == id)
                .Select(b => new BookDetailsDto
                {
                    Id = b.Id,
                    ISBN = b.ISBN,
                    Title = b.Title,
                    Length = b.Length,
                    Language = b.Language,
                    ReleaseDate = b.ReleaseDate,
                    Description = b.Description,
                    Authors = b.Authors.Select(a => a.Name).ToList(),
                    Tags = b.Tags.Select(t => t.Name).ToList(),
                    AverageRating = _context.CalculateAverageRating(b.Id),
                    ReviewCount = b.Reviews.Count
                })
                .FirstOrDefaultAsync();

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        // POST: api/books
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BookDetailsDto>> CreateBook(
            CreateBookDto dto)
        {

            //check on authors
            if (dto.AuthorIds.Count == 0)
                return BadRequest("A book must have at least one author.");

            var authors = await _context.Authors
                .Where(a => dto.AuthorIds.Contains(a.Id))
                .ToListAsync();

            if (authors.Count != dto.AuthorIds.Count)
                return BadRequest("One or more author IDs do not exist.");

            //check on tags
            List<Tag> tags = new();
            if (dto.TagIds != null) 
            {
                tags = await _context.Tags
                    .Where(t => dto.TagIds.Contains(t.Id))
                    .ToListAsync();

                if (tags.Count !=dto.TagIds.Count)
                    return BadRequest("One or more tag IDs do not exist.");
            }


                var book = new Book
            {
                ISBN = dto.ISBN,
                Title = dto.Title,
                Length = dto.Length,
                Language = dto.Language,
                ReleaseDate = dto.ReleaseDate,
                Description = dto.Description,
                Authors = authors,
                Tags = tags
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBook),
                new { id = book.Id },
                new BookDetailsDto
                {
                    Id = book.Id,
                    ISBN = book.ISBN,
                    Title = book.Title,
                    Length = book.Length,
                    Language = book.Language,
                    ReleaseDate = book.ReleaseDate,
                    Description = book.Description,
                    Authors = authors.Select(a => a.Name).ToList(),
                    Tags = tags.Select(t=>t.Name).ToList(),
                    AverageRating = null,
                    ReviewCount = 0
                }
            );
        }

        [HttpPost("{id}/tags")]
        [Authorize]
        public async Task<ActionResult<BookDetailsDto>> AddTagsToBook(int id, AddTagsToBookDto dto)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Tags)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                return NotFound("Book not found.");

            var tags = await _context.Tags
                .Where(t => dto.TagIds.Contains(t.Id))
                .ToListAsync();

            if (tags.Count != dto.TagIds.Count)
                return BadRequest("One or more tag IDs do not exist.");

            foreach (var tag in tags)
            {
                if (!book.Tags.Any(t => t.Id == tag.Id))
                    book.Tags.Add(tag);
            }

            await _context.SaveChangesAsync();

            return Ok(new BookDetailsDto
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Length = book.Length,
                Language = book.Language,
                ReleaseDate = book.ReleaseDate,
                Description = book.Description,
                Authors = book.Authors.Select(a => a.Name).ToList(),
                Tags = book.Tags.Select(t => t.Name).ToList(),
                AverageRating = _context.CalculateAverageRating(book.Id),
                ReviewCount = book.Reviews.Count
            });
        }


        [HttpDelete("{bookId}/tags/{tagId}")]
        [Authorize]
        public async Task<IActionResult> RemoveTagFromBook(int bookId, int tagId)
        {
            var book = await _context.Books
                .Include(b => b.Tags)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
                return NotFound("Book not found.");

            var tag = book.Tags.FirstOrDefault(t => t.Id == tagId);
            if (tag == null)
                return NotFound("Tag not assigned to this book.");

            book.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
