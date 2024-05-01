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
                case MenuOptions.Quit:
                    break;
                default:
                    isAppRunning = false;
                    break;
            }

        }
    }
}
