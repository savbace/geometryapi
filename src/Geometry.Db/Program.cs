using System.Reflection;
using DbUp;
using Microsoft.Data.SqlClient;
using Polly;
using Polly.Retry;

namespace Geometry.Db;

internal static class Program
{
    private static int Main(string[] args)
    {
        var connectionString = args.FirstOrDefault();

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            Console.WriteLine("Connection string is missed.");
            return -1;
        }

        EnsureDbCreated(connectionString);

        var upgrader =
            DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();

#if DEBUG
            Console.ReadLine();
#endif

            return -1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();
        return 0;
    }

    private static void EnsureDbCreated(string connectionString)
    {
        RetryPolicy retry = Policy
            .Handle<SqlException>()
            .WaitAndRetry(new[]
            {
                TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(7),
            },
            (__, _) => Console.WriteLine("Retrying SQL exception..."));

        retry.Execute(() => EnsureDatabase.For.SqlDatabase(connectionString));
    }
}