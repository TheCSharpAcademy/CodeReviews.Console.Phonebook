using Phonebook.Model;
using Spectre.Console;

namespace Phonebook;

internal class PhonebookService
{
	internal static void GetContacts()
	{
		var contacts = PhonebookController.GetContacts();
		UserInterface.DisplayContactTable(contacts);
	}

	internal static void InsertContact()
	{
		var contact = new Contact();
		contact.Name = AnsiConsole.Ask<string>("Contact's name:");
		contact.PhoneNumber = AnsiConsole.Ask<string>("Contact's phonenumber:");
		PhonebookController.AddContact(contact);
	}
}
