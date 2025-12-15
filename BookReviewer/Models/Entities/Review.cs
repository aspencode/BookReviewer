using System.ComponentModel.DataAnnotations;

namespace BookReviewer.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [Range(0, 20)]

        public int Rating { get; set; }
        public string? Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = default!;

        public int BookId { get; set; }
        public Book Book { get; set; } = default!;
    }

}
