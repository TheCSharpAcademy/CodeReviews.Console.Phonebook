using Spectre.Console;

using Phonebook.MartinL_no.Models;
using Phonebook.MartinL_no.Controllers;
using Phonebook.MartinL_no.UserInterfaces;

namespace Phonebook.MartinL_no.Services;

internal static class ContactService
{
    public static void AddContact()
	{
        var name = AnsiConsole.Ask<string>("Contact's name: ");
        var phoneNumber = AnsiConsole.Ask<string>("Phone number: ");
        var email = AnsiConsole.Ask<string>("Email address: ");

        ContactController.AddContact(new Contact { Name = name, PhoneNumber = phoneNumber, Email = email });
    }

    public static void DeleteContact()
	{
		var contact = GetContactOptionInput();
		ContactController.DeleteContact(contact);
	}

    public static void GetContacts()
    {
        var contacts = ContactController.GetContacts();
        UserInterface.ShowContacts(contacts);
    }

    public static void GetContact()
    {
        var contact = ContactService.GetContactOptionInput();
		UserInterface.ShowContact(contact);
    }

    public static void UpdateContact()
    {
        var contact = ContactService.GetContactOptionInput();
        contact.Name = AnsiConsole.Ask<string>("Contact's new name: ");
        contact.PhoneNumber = AnsiConsole.Ask<string>("Contact's new phone number: ");
        contact.Email = AnsiConsole.Ask<string>("Email address: ");

        ContactController.UpdateContact(contact);
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
