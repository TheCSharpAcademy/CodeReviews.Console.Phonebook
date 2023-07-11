using Spectre.Console;

namespace LONCHANICK_PhoneBookConsoleApp;

internal class ContactService
{
	internal static void AddContact()
	{
		Contact contact = new();
		contact.Name = AnsiConsole.Ask<string>("Name:");
		contact.PhoneNumber = AnsiConsole.Ask<string>("PhoneNumber:");
		ContactRepository.Add(contact);
	}

	internal static void RemoveContact()
	{
		var contact = ContactsUI.ShooseAnOptionCONTACTS();
		ContactRepository.Remove(contact);
	}

	internal static void UpdateContact()
	{
		var contact = ContactsUI.ShooseAnOptionCONTACTS();
		contact.Name = AnsiConsole.Confirm("Update Name field sure?")
			? AnsiConsole.Ask<string>("New Name: ")
			: contact.Name;

		contact.PhoneNumber = AnsiConsole.Confirm("Update Phone Number field sure?")
			? AnsiConsole.Ask<string>("New PhoneNumber: ")
			: contact.PhoneNumber;

		ContactRepository.Update(contact);
	}

	internal static void ViewAllContacts()
	{
		var contacts = ContactRepository.GetContacts();
		ContactsUI.ShowAllContacts(contacts);
	}

	internal static void ViewContact()//get a contact by Id
	{
		var contact = ContactsUI.ShooseAnOptionCONTACTS();
		ContactsUI.ShowSingleContactDetails(contact);
	}
}
