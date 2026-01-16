using System.Security.Claims;
using BookReviewer.Data;
using BookReviewer.Models.DTOs.Common;
using BookReviewer.Models.DTOs.Reviews;
using BookReviewer.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookReviewer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateReview(CreateReviewDto dto)
        {
            // 1. Pobranie ID użytkownika z tokena JWT
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();
            
            int userId = int.Parse(userIdClaim);

            // 2. Sprawdzenie czy książka istnieje
            var bookExists = await _context.Books.AnyAsync(b => b.Id == dto.BookId);
            if (!bookExists) return NotFound("Książka o podanym ID nie istnieje.");

            // 3. Opcjonalne: Sprawdzenie czy użytkownik już dodało recenzję tej książki
            var alreadyReviewed = await _context.Reviews
                .AnyAsync(r => r.BookId == dto.BookId && r.UserId == userId);
            if (alreadyReviewed) return BadRequest("Recenzja dla tej książki została już dodana przez Ciebie.");

            var review = new Review
            {
                Rating = dto.Rating,
                Description = dto.Description,
                BookId = dto.BookId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok("Recenzja została dodana.");
        }

        // GET: api/reviews/book/{bookId}?pageNumber=1&pageSize=10


        [HttpGet("book/{bookId}")]
        public async Task<ActionResult<PagedResult<ReviewListDto>>> getReviews(
            int bookId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {

            var query = _context.Reviews
                .Where(r => r.BookId == bookId);

            var totalCount = await query.CountAsync();

            var reviews = await query
                .OrderByDescending(r => r.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new ReviewListDto
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Description = r.Description,
                    CreatedAt = r.CreatedAt,
                    UserName = r.User.UserName ?? "Anonim"
                })
                .ToListAsync();

            var result = new PagedResult<ReviewListDto>
            {
                Items = reviews,
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };

            return Ok(result);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim);

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return NotFound();

            if (review.UserId != userId)
                return Forbid();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}