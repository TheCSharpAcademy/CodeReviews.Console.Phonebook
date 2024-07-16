using PhoneBook.Controllers;
using PhoneBook.Models;
using PhoneBook.Views;
using Spectre.Console;

namespace PhoneBook.Services
{
    public class EmailSMSService
    {
        internal static void SendEmail()
        {
            AnsiConsole.MarkupLine("[bold][green]Select Category where contact is.[/][/]");
            var category = CategoryService.GetCategoriesOptionInput();

            if (category == null)
            {
                AnsiConsole.MarkupLine("[red]No categories available. Add a new Category first before sending an email.[/]");
                MainMenu.ShowMainMenu();
                return;
            }

            var contactsList = ContactController.GetContactsByCategory(category.CategoryId);
            var contact = GetContactOptionInput(contactsList);

            if (contact == null)
            {
                AnsiConsole.MarkupLine("[red]No contacts found in this category.[/]");
                MainMenu.ShowMainMenu();
                return;
            }

            var emailDetails = UserInteraction.UserInteractions.GetEmailDetails();

            EmailSMSController.Send(emailDetails, contact);

            MainMenu.ShowMainMenu();
        }

        internal static Contact? GetContactOptionInput(List<Contact> contactList)
        {

            if (contactList == null || contactList.Count == 0)
            {
                return null;
            }

            var contactsArray = contactList.Select(c => $"{c.Name}: {c.Email}").ToArray();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[bold][yellow]Select a contact to send email to:[/][/]")
                    .AddChoices(contactsArray)
            );

            var email = choice.Split(':').Last().Trim();
            
            return contactList.Single(c => c.Email.Equals(email));
        }
    }
}