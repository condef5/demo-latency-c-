using System.Diagnostics;
using ConsoleApp1;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        using (var spannerSampleDbContext = new BloggingContext())
        {
            long total = 0;
            long iterations = 3;
            
            var isConnected = spannerSampleDbContext.Database.CanConnect();
            
            Console.WriteLine("Database connected: " + isConnected);
            
            var result = spannerSampleDbContext.Blogs
                .FromSqlRaw("SELECT 'my-id' AS BlogId, 'dd' AS Url")
                .AsEnumerable()
                .FirstOrDefault();
            
            for (int i = 0; i < iterations; i++)
            {
                var timer = Stopwatch.StartNew();
                timer.Start();
                result = spannerSampleDbContext.Blogs
                    .FromSqlRaw("SELECT '1' AS BlogId, 'dd' AS Url")
                    .AsEnumerable()
                    .FirstOrDefault();
                timer.Stop();
                total = total + timer.ElapsedMilliseconds;
            }
        
            Console.WriteLine($"Avg time for reading: {((double)total/iterations)} in milliseconds");
        }
        
    }
}