namespace BookReviewer.Models.DTOs.Authors
{
    public class AuthorDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public int BookCount { get; set; }
    }
}
