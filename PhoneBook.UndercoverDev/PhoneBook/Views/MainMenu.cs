using PhoneBook.Services;
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
                case MenuOptions.AddCategory:
                    CategoryService.AddCategory();
                    break;
                case MenuOptions.ViewCategories:
                    CategoryService.ViewCategories();
                    break;
                case MenuOptions.AddContact:
                    ContactService.AddContact();
                    break;
                case MenuOptions.ViewAllContacts:
                    ContactService.ViewAllContacts();
                    break;
                case MenuOptions.UpdateContact:
                    ContactService.UpdateContact();
                    break;
                case MenuOptions.DeleteContact:
                    ContactService.DeleteContact();
                    break;
                case MenuOptions.Exit:
                    UserInteractions.Exit();
                    break;
            }
        }
    }
}