using System.ComponentModel.DataAnnotations;

namespace BookReviewer.Models.DTOs.Authors
{
    public class CreateAuthorDto
    {
        [Required]
        public string Name { get; set; } = default!;
    }
}
