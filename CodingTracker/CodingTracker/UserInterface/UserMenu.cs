using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingTracker.Controllers;
using CodingTracker.Enums;
using Spectre;
using Spectre.Console;

namespace CodingTracker.UserInterface;

internal class UserMenu
{
    private CodingSessionController _controller = new CodingSessionController();
    public void ShowMenu()
    {
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

    private void ViewAll()
    {
        Console.Clear();

        var AllCodingSessions = _controller.GetAll();
        var table = new Table();
        table.AddColumns("Id", "StartTime", "EndTime", "Duration");

        foreach (var session in AllCodingSessions)
        {
            table.AddRow([session.Id.ToString(), session.StartTime.ToString("dd-MM-yyyy-HH-mm-ss"), session.EndTime.ToString("dd-MM-yyyy-HH-mm-ss"), session.Duration.ToString("dd-MM-yyyy-HH-mm-ss")]);
        }

        AnsiConsole.Write(table);
    }

    private void Add()
    {
        Console.Clear();

        var startTime = AnsiConsole.Ask<DateTime>("What time did you start(dd-MM-yyyy hh:mm): ");
        var endTime = AnsiConsole.Ask<DateTime>("What time did you end(dd-MM-yyyy hh:mm): ");
        var duration = endTime - startTime;

        Console.WriteLine(duration.ToString(@"hh\:mm\:ss")); //Dit is hoe je een timespan format
    }
}
