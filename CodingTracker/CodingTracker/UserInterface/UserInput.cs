using CodingTracker.Controllers;
using CodingTracker.Models;
using Spectre.Console;
using System.Diagnostics;

namespace CodingTracker.UserInterface;

internal static class UserInput
{
    private static readonly CodingSessionController _controller = new CodingSessionController();
    public static void ViewAll()
    {
        var AllCodingSessions = _controller.GetAll();

        PrintCodingSessions(AllCodingSessions);

        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public static void Add()
    {
        var session = AskForInput();

        _controller.Add(session);
    }

    public static void Update()
    {
        var AllCodingSessions = _controller.GetAll();

        PrintCodingSessions(AllCodingSessions);

        int id;
        do
        {
            id = AnsiConsole.Ask<int>("Choose the [yellow]Id[/] of a session to update.");
        }
        while (!_controller.Exists(id));

        var session = AskForInput();
        session.Id = id;

        _controller.Update(session);
    }

    public static void Delete()
    {
        var AllCodingSessions = _controller.GetAll();

        PrintCodingSessions(AllCodingSessions);
        int id;

        do
        {
            id = AnsiConsole.Ask<int>("Choose a valid [yellow]Id[/] of a session to delete.");
        }
        while (!_controller.Exists(id));

        _controller.Delete(id);
    }

    public static void StartCodingSession()
    {
        Console.WriteLine("press any key to start the session");
        Console.ReadKey(true);

        Stopwatch stopwatch = Stopwatch.StartNew();
        stopwatch.Start();

        var startTime = DateTime.Now;
        startTime = new DateTime(
                startTime.Ticks - (startTime.Ticks % TimeSpan.TicksPerSecond),
                startTime.Kind
                );

        Console.WriteLine("press enter to stop the session");

        ConsoleKeyInfo input;
        do
        {
            input = Console.ReadKey();
        }
        while (input.Key is not ConsoleKey.Enter);

        stopwatch.Stop();
        var endTime = DateTime.Now;
        endTime = new DateTime(
                endTime.Ticks - (endTime.Ticks % TimeSpan.TicksPerSecond),
                endTime.Kind
                );

        var duration = new TimeSpan(stopwatch.Elapsed.Ticks / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond); //truncate the milliseconds

        string elapsedTime = $"{duration.Hours:00}:{duration.Minutes:00}:{duration.Seconds:00}";
        Console.WriteLine("Codingtime: " + elapsedTime);
        Console.ReadKey();

        var session = new CodingSession()
        {
            StartTime = startTime,
            EndTime = endTime,
            Duration = duration.TotalSeconds
        };

        _controller.Add(session);
    }

    private static string ConvertDateToString(DateTime date)
    {
        return date.ToString("dd-MM-yyyy HH:mm");
    }

    private static string ConvertTimeSpanToString(double duration)
    {
        return TimeSpan.FromSeconds(duration).ToString();
    }

    private static void PrintCodingSessions(List<CodingSession> sessions)
    {
        var table = new Table();
        table.AddColumns("Id", "StartTime", "EndTime", "Duration");

        foreach (var session in sessions)
        {
            var startTime = ConvertDateToString(session.StartTime);
            var endTime = ConvertDateToString(session.EndTime);
            var duration = ConvertTimeSpanToString(session.Duration);

            table.AddRow([session.Id.ToString(), startTime, endTime, duration]);
        }

        AnsiConsole.Write(table);
    }

    private static CodingSession AskForInput()
    {
        var startTime = AnsiConsole.Ask<DateTime>("What time did you start(mm-dd-yyyy hh:mm): ");
        var endTime = AnsiConsole.Ask<DateTime>("What time did you end(mm-dd-yyyy hh:mm): ");
        while (DateTime.Compare(startTime, endTime) > 0)
        {
            Console.WriteLine("EndTime can not be before the starttime.");
            endTime = AnsiConsole.Ask<DateTime>("What time did you end(mm-dd-yyyy hh:mm): ");
        }

        var duration = endTime - startTime;

        CodingSession session = new CodingSession()
        {
            StartTime = startTime,
            EndTime = endTime,
            Duration = duration.TotalSeconds
        };

        return session;
    }

    
}
