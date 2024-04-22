
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ConsoleApp1;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        NpgsqlConnectionStringBuilder urlBuilder = new NpgsqlConnectionStringBuilder();
            
        urlBuilder.Host = "pub-us-west1.cd6db360-69b9-4c4b-96b8-b2e15bc9c615.gcp.ybdb.io";
        urlBuilder.Port = 5433;
        urlBuilder.Database = "yugabyte";
        urlBuilder.Username = "admin";
        urlBuilder.Password = "7JmK5Et6pEclBJC-kaLqBwTPLHdSFu";

        // On every new connection the NpgSQL driver makes extra system table queries to map types, which adds overhead.
        // To turn off this behavior, set the following option in your connection string.
        urlBuilder.ServerCompatibilityMode = ServerCompatibilityMode.NoTypeLoading;

        optionsBuilder.UseNpgsql(urlBuilder.ConnectionString);
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