using System.Diagnostics;
using ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        // // Start the stopwatch
        Stopwatch stopwatch = Stopwatch.StartNew();
        //
        // // Your Entity Framework code to establish the connection
        // using (var dbContext = new MyDbContext())
        // {
        //     // This will trigger the actual connection to the database
        //     dbContext.Database.CanConnect();
        // }
        //
        // // Stop the stopwatch
        using (var spannerSampleDbContext = new BloggingContext())
        {
            // This will trigger the actual connection to the database
            spannerSampleDbContext.Database.CanConnect();
        }
        
        stopwatch.Stop();
        //
        // // Output the elapsed time
        Console.WriteLine($"Time taken to establish connection: {stopwatch.ElapsedMilliseconds} milliseconds");

    }
}