using Spectre.Console;
using static Phonebook.Enums;

namespace Phonebook;
internal class Menu
{
    internal void MainMenu()
    {
        var exit = false;

        while (!exit)
        {
            AnsiConsole.Clear();

            AnsiConsole.Write(
                new FigletText("Phonebook")
                    .LeftJustified()
                    .Color(Color.Yellow));

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                .Title("Select from the menu:")
                .AddChoices(Enum.GetValues<MenuOptions>()));

            switch (selection)
            {
                case MenuOptions.ViewAllContacts:
                    ContactService.GetContacts();
                    break;
                case MenuOptions.ViewContact:
                    ContactService.GetContact();
                    break;
                case MenuOptions.AddContact:
                    ContactService.InsertContact();
                    break;
                case MenuOptions.DeleteContact:
                    ContactService.DeleteContact();
                    break;
                case MenuOptions.UpdateContact:
                    ContactService.UpdateContact();
                    break;
                case MenuOptions.Exit:
                    if (AnsiConsole.Confirm("Are you sure you want to exit?"))
                    {
                        Console.WriteLine("Goodbye!");
                        exit = true;
                    }
                    else
                    {
                        exit = false;
                    }
                    break;
            }
        }
    }
}
