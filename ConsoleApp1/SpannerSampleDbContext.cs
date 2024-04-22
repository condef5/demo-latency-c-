
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Host=pub-europe-west1.4b92a469-904b-4959-9fe5-803d281553fc.gcp.ybdb.io;Port=5433;Database=yugabyte;Username=admin;Timeout=1000;CommandTimeout=1000;Password=NCKshEehIQwBdP5wyunzpIIKacGshu";
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId);
        });
    }
}

public class Blog
{
    [Required]
    public string BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; } = new List<Post>();
}

public class Post
{
    public string PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public Guid BlogId { get; set; }
    public Blog Blog { get; set; }
}