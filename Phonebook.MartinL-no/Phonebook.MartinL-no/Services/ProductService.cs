using Phonebook.MartinL_no.Models;
using Phonebook.MartinL_no.Controllers;
using Phonebook.MartinL_no.UserInterfaces;

using Spectre.Console;

namespace Phonebook.MartinL_no.Services;

internal static class ContactService
{
    public static void AddContact()
	{
        var name = AnsiConsole.Ask<string>("Contact's name: ");
        var phoneNumber = AnsiConsole.Ask<string>("Phone number: ");
        ContactController.AddContact(new Contact { Name = name, PhoneNumber = phoneNumber });
    }

    public static void DeleteContact()
	{
		var contact = GetContactOptionInput();
		ContactController.DeleteContact(contact);
	}

    internal static void GetContacts()
    {
        var contacts = ContactController.GetContacts();
        UserInterface.ShowContacts(contacts);
    }

    internal static void GetContact()
    {
        var contact = ContactService.GetContactOptionInput();
		UserInterface.ShowContact(contact);
    }

    private static Contact GetContactOptionInput()
	{
		var contacts = ContactController.GetContacts();
		var contactsArray = contacts.Select(x => x.Name).ToArray();
		var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
			.Title("Choose Contact")
			.AddChoices(contactsArray));
		var id = contacts.Single(x => x.Name == option).Id;
		var contact = ContactController.GetContactById(id);

		return contact;
	}
}

