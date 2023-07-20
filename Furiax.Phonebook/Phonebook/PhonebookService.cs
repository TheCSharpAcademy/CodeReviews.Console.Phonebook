using Phonebook.Model;
using Spectre.Console;

namespace Phonebook;

internal class PhonebookService
{
	internal static void GetContact()
	{
		var contact = GetContactOptionInput();
		UserInterface.DisplayContactTable(contact);
	}

	private static Contact GetContactOptionInput()
	{
		var contacts = PhonebookController.GetContacts();
		var contactsArray = contacts.Select(x => x.Name).ToArray();
		var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
			.Title("Choose contact")
			.AddChoices(contactsArray));
		var id = contacts.Single(x => x.Name == option).Id;
		var contact = PhonebookController.GetContactById(id);
		return contact;
	}

	internal static void GetContacts()
	{
		var contacts = PhonebookController.GetContacts();
		UserInterface.DisplayContactTable(contacts);
	}

	internal static void InsertContact()
	{
		var contact = new Contact();
		contact.Name = AnsiConsole.Ask<string>("Contact's name:");
		string phoneNumber;
		do
		{
			phoneNumber = AnsiConsole.Ask<string>("Contact's phonenumber:");
			if (Validation.IsValidPhoneNumber(phoneNumber))
				contact.PhoneNumber = phoneNumber;
			else
				Console.WriteLine("Invalid phonenumber, a valid phonenumber can only contain digits. Try again");
		} while (!Validation.IsValidPhoneNumber(phoneNumber));

		string emailAddress;
		do
		{
			emailAddress = AnsiConsole.Ask<string>("Contact's email address:");
			if (Validation.IsValidEmail(emailAddress))
				contact.EmailAddress = emailAddress;
			else
				Console.WriteLine("Invalid emailadres, try again.");
		}while (!Validation.IsValidEmail(emailAddress));
		PhonebookController.AddContact(contact);
		Console.Clear();
	}

	internal static void DeleteContact()
	{
		var contact = GetContactOptionInput();
		PhonebookController.DeleteContact(contact);
	}
	internal static void UpdateContact()
	{
		var contact = GetContactOptionInput();
		contact.Name = AnsiConsole.Confirm("Update name?") ?
			AnsiConsole.Ask<string>("Enter the new name:")
			: contact.Name;
		contact.PhoneNumber = AnsiConsole.Confirm("Update phonenumber?") ?
			AnsiConsole.Ask<string>("Enter the new phonenumber:")
			: contact.PhoneNumber;
		contact.EmailAddress = AnsiConsole.Confirm("Update emailaddress?") ?
			AnsiConsole.Ask<string>("Enter the new emailaddress:")
			: contact.EmailAddress;
		PhonebookController.UpdateContact(contact);
		Console.Clear();
	}
}
