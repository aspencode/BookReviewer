using System.ComponentModel.DataAnnotations;

namespace BookReviewer.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [MaxLength(13)]
        public string ISBN { get; set; } = default!;

        public string Title { get; set; } = default!;
        public int Length { get; set; }
        public string Language { get; set; } = default!;
        public DateOnly ReleaseDate { get; set; }
        public string? Description { get; set; }


        // Many-to-many relationships
        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        // One-to-many relationship
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}