using System.ComponentModel.DataAnnotations;
namespace BookReviewer.Models.DTOs.Reviews
{
    public class CreateReviewDto
    {

        [Range(0, 20, ErrorMessage = "Ocena musi być w przedziale 0-20.")]
        public int Rating { get; set; }
        public string? Description { get; set; }

        [Required]
        public int BookId { get; set; }

    }
}
