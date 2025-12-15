using BookReviewer.Data;
using BookReviewer.Models.Entities;
using BookReviewer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookReviewer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        // Inject both DbContext and JwtService
        public AuthController(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // Registration endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
                return BadRequest("Username already exists.");

            var (hash, salt) = PasswordHelper.CreatePasswordHash(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        // <--- Put the login endpoint here
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // 1. Find user in DB
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            // 2. Verify password
            bool validPassword = PasswordHelper.VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt);
            if (!validPassword)
                return Unauthorized("Invalid username or password.");

            // 3. Generate JWT
            string token = _jwtService.GenerateToken(user.Username, user.Role);

            // 4. Return token to client
            return Ok(new { token });
        }
    }

    // DTOs
    public record RegisterDto(string Username, string Email, string Password);
    public record LoginDto(string Username, string Password);
}
