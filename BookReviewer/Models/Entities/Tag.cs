namespace BookReviewer.Models.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }

}
