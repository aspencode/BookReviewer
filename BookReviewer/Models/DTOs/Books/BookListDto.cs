namespace BookReviewer.Models.DTOs.Books
{
    public class BookListDto
    {
        public int Id { get; set; }
        public string ISBN { get; set; } = default!;
        public string Title { get; set; } = default!;

        public double? AverageRating { get; set; }
        public List<string> Authors { get; set; } = new();
    }
}
