using Phonebook.Model;
using Spectre.Console;
using static Phonebook.Enums;

namespace Phonebook;

internal class UserInterface
{
	internal static void DisplayContactTable(List<Contact> contacts)
	{
		var table = new Table();
		table.AddColumn("Id");
		table.AddColumn("Name");
		table.AddColumn("Phonenumber");
		table.AddColumn("Emailaddress");

		foreach (var contact in contacts)
		{
			table.AddRow(contact.Id.ToString(), contact.Name, contact.PhoneNumber, contact.EmailAddress);
		}

		AnsiConsole.Write(table);

        Console.WriteLine("Press any key to continue");
		Console.ReadLine();
		Console.Clear();
    }

	internal static void DisplayContactTable(Contact contact)
	{
		var panel = new Panel($@"Id: {contact.Id}
Name: {contact.Name}
Phonenumber: {contact.PhoneNumber}
Emailaddress= {contact.EmailAddress}");
		panel.Header = new PanelHeader("Contact info");
		panel.Padding = new Padding(2,2,2,2);

		AnsiConsole.Write(panel);

		Console.WriteLine("Press any key to continue");
		Console.ReadLine();
		Console.Clear();
	}

	internal static void MainMenu()
	{
		bool appAlive = true;
		while (appAlive)
		{
			var option = AnsiConsole.Prompt(new SelectionPrompt<MenuOptions>()
				.Title("What do you want to do ?")
				.AddChoices(
					MenuOptions.AddContact,
					MenuOptions.ShowContact,
					MenuOptions.ShowAllContacts,
					MenuOptions.UpdateContact,
					MenuOptions.DeleteContact,
					MenuOptions.ExitApplication));

			switch (option)
			{
				case MenuOptions.AddContact:
					PhonebookService.InsertContact();
					break;
				case MenuOptions.ShowContact:
					PhonebookService.GetContact();
					break;
				case MenuOptions.ShowAllContacts:
					PhonebookService.GetContacts();
					break;
				case MenuOptions.UpdateContact:
					PhonebookService.UpdateContact();
					break;
				case MenuOptions.DeleteContact:
					PhonebookService.DeleteContact();
					break;
				case MenuOptions.ExitApplication:
					appAlive = false;
					break;
			}
		}
	}
}
