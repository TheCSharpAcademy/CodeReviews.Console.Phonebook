using Spectre.Console;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace PhoneBook_CRUD
{
    internal class ContactService
    {
        static internal Contacts GetContactOptionInput()
        {
            var contacts = ContactController.ShowAllContacts();
            var contactsArray = contacts.Select(x => x.Name).ToArray();

            var option = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Choose a contact").AddChoices(contactsArray));

            var id = contacts.Single(x => x.Name == option).Id;
            var contact = ContactController.SearchContactById(id);

            return contact;
        }

        internal static void DeleteContact()
        {
            var contact = GetContactOptionInput();
            ContactController.DeleteContact(contact);
        }

        static internal void ShowAllContacts()
        {
            var contacts = ContactController.ShowAllContacts();
            UserInterface.ShowContactsTable(contacts);
        }
        internal static void SearchContact()
        {
            var contact = ContactService.GetContactOptionInput();
            ContactController.ShowContact(contact);
        }

        internal static void UpdateContact()
        {
            var contact = GetContactOptionInput();
            string name = AnsiConsole.Ask<string>("Contact's new name:");
            string email = AnsiConsole.Ask<string>("Contact's new email:");
            string phoneNumber = AnsiConsole.Ask<string>("Contact's new phone number:");
            ContactController.UpdateContact(name, email, phoneNumber);
        }

        //usar sqlserver
    }
}
