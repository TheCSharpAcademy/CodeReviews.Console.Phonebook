using Spectre.Console;
using Phonebook.samggannon.Services;
using static Phonebook.samggannon.Enums;

namespace Phonebook.samggannon;

internal static class UserInterface
{
    static internal void MainMenu()
    {
        var isAppRunning = true;
        while (isAppRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                .Title("What would you like to do?")
                .AddChoices(
                    MenuOptions.AddContact,
                    MenuOptions.Quit));

            switch (option)
            {
                case MenuOptions.AddContact:
                    ContactsService.AddContact();
                    break;
                case MenuOptions.ViewAllContacts:
                    ContactsService.ViewAllContacts();
                    break;
                case MenuOptions.ViewContact:
                    ContactsService.ViewContact();
                    break;
                case MenuOptions.UpdateContact:
                    ContactsService.UpdateContact();
                    break;
                case MenuOptions.DeleteContact:
                    ContactsService.DeleteContact();
                    break;
                case MenuOptions.Quit:
                    isAppRunning = false;
                    Environment.Exit(0);
                    break;
                default:
                    isAppRunning = false;
                    break;
            }

        }
    }
}
