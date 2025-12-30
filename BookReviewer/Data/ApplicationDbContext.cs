using BookReviewer.Models.Entities;
using BookReviewer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookReviewer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Tag> Tags => Set<Tag>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Set default schema for all EF tables
            //modelBuilder.HasDefaultSchema("bookdb_schema");

            // -------------------------
            // Book ↔ Tag many-to-many
            // -------------------------
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Tags)
                .WithMany(t => t.Books)
                .UsingEntity(j => j.ToTable("BookTag"));

            // -------------------------
            // Book ↔ Author many-to-many
            // -------------------------
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity(j => j.ToTable("BookAuthor"));

            // -------------------------
            // Review rating check constraint (PostgreSQL)
            // -------------------------
            modelBuilder.Entity<Review>()
                .ToTable(t => t.HasCheckConstraint(
                    "CK_Review_Rating",
                    "\"Rating\" >= 0 AND \"Rating\" <= 20"));
        }
    }
}
