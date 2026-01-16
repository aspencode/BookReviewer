using Microsoft.AspNetCore.Identity;
using BookReviewer.Models.Entities;

namespace BookReviewer.Models.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
