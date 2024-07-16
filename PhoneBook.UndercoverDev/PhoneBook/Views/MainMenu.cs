using PhoneBook.Services;
using PhoneBook.UserInteraction;
using Spectre.Console;
using static PhoneBook.MenuEnum;

namespace PhoneBook.Views
{
    public class MainMenu
    {
        public static void ShowMainMenu()
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                .Title("\n[bold][blue]Phone Book Application Menu[/][/]")
                .PageSize(6)
                .AddChoices(
                    MenuOptions.AddCategory,
                    MenuOptions.ViewCategories,
                    MenuOptions.EditCategories,
                    MenuOptions.DeleteCategory,
                    MenuOptions.AddContact,
                    MenuOptions.ViewAllContacts,
                    MenuOptions.UpdateContact,
                    MenuOptions.DeleteContact,
                    MenuOptions.SearchContactsByCategory,
                    MenuOptions.SendEmail,
                    MenuOptions.SendSMS,
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
                case MenuOptions.EditCategories:
                    CategoryService.EditCategories();
                    break;
                case MenuOptions.DeleteCategory:
                    CategoryService.DeleteCategory();
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
                case MenuOptions.SearchContactsByCategory:
                    ContactService.SearchContactsByCategory();
                    break;
                case MenuOptions.SendEmail:
                    EmailSMSService.SendEmail();
                    break;
                case MenuOptions.SendSMS:
                    ContactService.SendSMS();
                    break;
                case MenuOptions.Exit:
                    UserInteractions.Exit();
                    break;
            }
        }
    }
}