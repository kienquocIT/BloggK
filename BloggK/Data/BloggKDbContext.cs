using BloggK.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BloggK.Data
{
    public class BloggKDbContext : DbContext
    {
        public BloggKDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlogPost>()
                .HasMany(b => b.Tags)
                .WithMany(t => t.BlogPosts)
                .UsingEntity<Dictionary<string, object>>(
                    "BlogPostTag",
                    j => j
                        .HasOne<Tag>()
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<BlogPost>()
                        .WithMany()
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                );
        }
    }
}
