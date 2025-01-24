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
                    ViewAll();
                    break;

                case MenuItems.Add:
                    Add();
                    break;

                case MenuItems.Update:
                    break;
                case MenuItems.Delete:
                    break;
                case MenuItems.Quit:
                    break;
                default:
                    break;
            }
        }
    }

    private void ViewAll()
    {
        Console.Clear();

        var AllCodingSessions = _controller.GetAll();
        var table = new Table();
        table.AddColumns("Id", "StartTime", "EndTime", "Duration");

        foreach (var session in AllCodingSessions)
        {
            var startTime = ConvertDateToString(session.StartTime);
            var endTime = ConvertDateToString(session.EndTime);
            var duration = ConvertTimeSpanToString(session.Duration);

            table.AddRow([session.Id.ToString(), startTime, endTime, duration]);
        }

        AnsiConsole.Write(table);
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    private void Add()
    {
        Console.Clear();

        var startTime = AnsiConsole.Ask<DateTime>("What time did you start(dd-MM-yyyy hh:mm): ");
        var endTime = AnsiConsole.Ask<DateTime>("What time did you end(dd-MM-yyyy hh:mm): ");
        var duration = endTime - startTime;

        CodingSession session = new CodingSession()
        {
            StartTime = startTime,
            EndTime = endTime,
            Duration = duration.TotalSeconds
        };

        _controller.Add(session);

        //Console.WriteLine(duration.ToString(@"hh\:mm\:ss")); //Dit is hoe je een timespan format
    }

    private string ConvertDateToString(DateTime date)
    {
        return date.ToString("dd-MM-yyyy HH:mm");
    }

    private string ConvertTimeSpanToString(double duration)
    {
        return TimeSpan.FromSeconds(duration).ToString();
    }
}
