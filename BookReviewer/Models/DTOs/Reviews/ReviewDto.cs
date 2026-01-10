namespace BookReviewer.Models.DTOs.Reviews
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; } = default!;
    }
}