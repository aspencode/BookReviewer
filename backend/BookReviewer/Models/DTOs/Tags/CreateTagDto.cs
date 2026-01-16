using System.ComponentModel.DataAnnotations;

namespace BookReviewer.Models.DTOs.Tags
{
    public class CreateTagDto
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
