namespace BookReviewer.Models.DTOs.Books
{
    public class AddTagsToBookDto
    {
        public List<int> TagIds { get; set; } = new();

    }
}
