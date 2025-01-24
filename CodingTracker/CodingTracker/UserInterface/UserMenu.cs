using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingTracker.Enums;
using Spectre;
using Spectre.Console;

namespace CodingTracker.UserInterface;

internal class UserMenu
{
    public void ShowMenu()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<MenuItems>()
            .Title("Pick an option")
            .AddChoices(Enum.GetValues<MenuItems>()));

        switch (choice)
        {
            case MenuItems.ViewAll:
                break;
            case MenuItems.Add:
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
