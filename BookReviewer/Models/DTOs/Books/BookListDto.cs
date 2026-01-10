namespace BookReviewer.Models.DTOs.Books
{
    public class BookListDto
    {
        public int Id { get; set; }
        public string ISBN { get; set; } = default!;
        public string Title { get; set; } = default!;

        public decimal? AverageRating { get; set; }

        public string? ImageUrl { get; set; }
        public List<string> Authors { get; set; } = new();
    }
}
