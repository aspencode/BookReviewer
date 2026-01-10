using BookReviewer.Models.DTOs.Reviews;

namespace BookReviewer.Models.DTOs.Books
{
    public class BookDetailsDto
    {
        public int Id { get; set; }
        public string ISBN { get; set; } = default!;
        public string Title { get; set; } = default!;
        public int Length { get; set; }
        public string Language { get; set; } = default!;
        public DateOnly ReleaseDate { get; set; }
        public string? Description { get; set; }

        public List<string> Authors { get; set; } = new();
        public List<string> Tags { get; set; } = new();

        public double? AverageRating { get; set; }
        public int ReviewCount { get; set; }
        public List<ReviewDto> Reviews { get; set; } = new();
        
    }
}
