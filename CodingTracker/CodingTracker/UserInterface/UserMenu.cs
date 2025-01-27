using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingTracker.Controllers;
using CodingTracker.Enums;
using CodingTracker.Models;
using Spectre;
using Spectre.Console;

namespace CodingTracker.UserInterface;

internal class UserMenu
{
    private CodingSessionController _controller = new CodingSessionController();
    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();

            var choice = AnsiConsole.Prompt(
            new SelectionPrompt<MenuItems>()
            .Title("Pick an option")
            .AddChoices(Enum.GetValues<MenuItems>()));

            switch (choice)
            {
                case MenuItems.ViewAll:
                    ClearConsole();
                    ViewAll();
                    break;

                case MenuItems.Add:
                    ClearConsole();
                    Add();
                    break;

                case MenuItems.Update:
                    ClearConsole();
                    Update();
                    break;

                case MenuItems.Delete:
                    ClearConsole();
                    Delete();
                    break;

                case MenuItems.Quit:
                    Environment.Exit(0);
                    break;

                default:
                    break;
            }
        }
    }

    private void ViewAll()
    {
        var AllCodingSessions = _controller.GetAll();

        PrintCodingSessions(AllCodingSessions);

        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    private void Add()
    {
        var session = GetUserCodingSession();

        _controller.Add(session);

        //Console.WriteLine(duration.ToString(@"hh\:mm\:ss")); //Dit is hoe je een timespan format
    }

    private void Update()
    {
        var AllCodingSessions = _controller.GetAll();

        PrintCodingSessions(AllCodingSessions);

        int id;
        do
        {
            id = AnsiConsole.Ask<int>("Choose the [yellow]Id[/] of a session to update.");
        }
        while (!_controller.Exists(id));

        var session = GetUserCodingSession();
        session.Id = id;

        _controller.Update(session);
    }

    private void Delete()
    {
        var AllCodingSessions = _controller.GetAll();

        PrintCodingSessions(AllCodingSessions);
        int id;

        do
        {
            id = AnsiConsole.Ask<int>("Choose the [yellow]Id[/] of a session to delete.");
        }
        while (!_controller.Exists(id));

        _controller.Delete(id);
    }

    private string ConvertDateToString(DateTime date)
    {
        return date.ToString("dd-MM-yyyy HH:mm");
    }

    private string ConvertTimeSpanToString(double duration)
    {
        return TimeSpan.FromSeconds(duration).ToString();
    }

    private void PrintCodingSessions(List<CodingSession> sessions)
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

    private CodingSession GetUserCodingSession()
    {
        var startTime = AnsiConsole.Ask<DateTime>("What time did you start(mm-dd-yyyy hh:mm): ");
        var endTime = AnsiConsole.Ask<DateTime>("What time did you end(mm-dd-yyyy hh:mm): ");
        var duration = endTime - startTime;

        CodingSession session = new CodingSession()
        {
            StartTime = startTime,
            EndTime = endTime,
            Duration = duration.TotalSeconds
        };

        return session;
    }

    private void ClearConsole()
    {
        Console.Clear();
    }
}
