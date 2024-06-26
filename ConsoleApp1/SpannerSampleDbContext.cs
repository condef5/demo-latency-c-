
using System.ComponentModel.DataAnnotations.Schema;
using Google.Cloud.EntityFrameworkCore.Spanner.Extensions;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    // Configures Entity Framework to use the specified Cloud Spanner database.
    {
        string projectId = "kuroro-beasts";
        string instanceId = "adasdas";
        string databaseId = "eleven";
        string connectionString =
            $"Data Source=projects/{projectId}/instances/{instanceId}/"
            + $"databases/{databaseId}";
      options.UseSpanner(connectionString);
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