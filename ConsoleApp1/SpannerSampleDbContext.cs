
using Google.Cloud.EntityFrameworkCore.Spanner.Extensions;
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
        string instanceId = "testing";
        string databaseId = "lucas";
        string connectionString =
            $"Data Source=projects/{projectId}/instances/{instanceId}/"
            + $"databases/{databaseId}";
      options.UseSpanner(connectionString);
    }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; } = new List<Post>();
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}