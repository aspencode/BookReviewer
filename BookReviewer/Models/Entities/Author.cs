using System.ComponentModel.DataAnnotations;

namespace BookReviewer.Models.Entities
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        // Many-to-many relationship
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}