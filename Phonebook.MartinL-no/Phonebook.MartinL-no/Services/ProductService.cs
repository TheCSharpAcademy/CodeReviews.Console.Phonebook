using Phonebook.MartinL_no.Controllers;
using Phonebook.MartinL_no.Models;
using Spectre.Console;

namespace Phonebook.MartinL_no.Services;

static internal class ContactService
{
	static public Contact GetContactOptionInput()
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

