using PhoneBook.UserInteraction;
using Spectre.Console;
using static PhoneBook.MenuEnum;

namespace PhoneBook.Views
{
    public class MainMenu
    {
        public static void ShowMAinMenu()
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                .Title("\n[bold][blue]Phone Book Application Menu[/][/]")
                .AddChoices(
                    MenuOptions.AddContact,
                    MenuOptions.ViewAllContacts,
                    MenuOptions.UpdateContact,
                    MenuOptions.DeleteContact,
                    MenuOptions.Exit
                )
            );

            switch (option)
            {
                case MenuOptions.AddContact:
                    break;
                case MenuOptions.ViewAllContacts:
                    break;
                case MenuOptions.UpdateContact:
                    break;
                case MenuOptions.DeleteContact:
                    break;
                case MenuOptions.Exit:
                    UserInteractions.Exit();
                    break;
            }
        }
    }
}