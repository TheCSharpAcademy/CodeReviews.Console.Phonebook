using Phonebook.StanimalTheMan.Services;
using Spectre.Console;
using static Phonebook.StanimalTheMan.Enums;

namespace Phonebook.StanimalTheMan
{
    internal class UserInterface
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
                        MenuOptions.DeleteContact,
                        MenuOptions.UpdateContact,
                        MenuOptions.ViewAllContacts,
                        MenuOptions.ViewContact,
                        MenuOptions.Quit));

                switch (option)
                {
                    case MenuOptions.AddContact:
                        ContactService.InsertContact();
                        break;
                    case MenuOptions.DeleteContact:
                        break;
                    case MenuOptions.UpdateContact:
                        break;
                    case MenuOptions.ViewAllContacts:
                        break;
                    case MenuOptions.ViewContact:
                        break;
                    case MenuOptions.Quit:
                        Console.WriteLine("Goodbye");
                        isAppRunning = false;
                        break;
                }
            }
        }
    }
}
