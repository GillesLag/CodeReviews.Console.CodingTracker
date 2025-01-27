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
                case MenuItems.StartCodingSession:
                    ClearConsole();
                    UserInput.StartCodingSession();
                    break;

                case MenuItems.ViewAll:
                    ClearConsole();
                    UserInput.ViewAll();
                    break;

                case MenuItems.Add:
                    ClearConsole();
                    UserInput.Add();
                    break;

                case MenuItems.Update:
                    ClearConsole();
                    UserInput.Update();
                    break;

                case MenuItems.Delete:
                    ClearConsole();
                    UserInput.Delete();
                    break;

                case MenuItems.Quit:
                    Environment.Exit(0);
                    break;

                default:
                    break;
            }
        }
    }

    private void ClearConsole()
    {
        Console.Clear();
    }
}
