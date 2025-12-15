using BookReviewer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookReviewer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        public DbSet<User> Users => Set<User>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Tag> Tags => Set<Tag>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Book ↔ Tag many-to-many
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Tags)
                .WithMany(t => t.Books);

            // Review rating check constraint (PostgreSQL)
            modelBuilder.Entity<Review>()
        .ToTable(t => t.HasCheckConstraint(
            "CK_Review_Rating",
            "\"Rating\" >= 0 AND \"Rating\" <= 20"));
        }

    }
}
