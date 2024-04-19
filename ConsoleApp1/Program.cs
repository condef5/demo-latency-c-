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
            long iterations = 100;
            
            spannerSampleDbContext.Database.CanConnect();
            
            for (int i = 0; i < iterations; i++)
            {
                var timer = Stopwatch.StartNew();
                timer.Start();
                spannerSampleDbContext.Blogs.FromSqlRaw("SELECT 1");
                timer.Stop();
                total = total + timer.ElapsedMilliseconds;
            }
        
            Console.WriteLine($"Avg time: {((double)total/iterations)} in milliseconds");
        }
        
    }
}