namespace BookReviewer.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public byte[] PasswordHash { get; set; } = default!;
        public byte[] PasswordSalt { get; set; } = default!;

        public string Role { get; set; } = "User";

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }


}