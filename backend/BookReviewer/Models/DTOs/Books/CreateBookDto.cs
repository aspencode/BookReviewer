using System.ComponentModel.DataAnnotations;

namespace BookReviewer.Models.DTOs.Books
{
    public class CreateBookDto
    {
        [Required]
        [MaxLength(13)]
        public string ISBN { get; set; } = default!;

        [Required]
        public string Title { get; set; } = default!;

        public int Length { get; set; }

        public string Language { get; set; } = default!;

        public DateOnly ReleaseDate { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public List<int> AuthorIds { get; set; } = new();
        public List<int> TagIds { get; set; } = new();
    }
}
