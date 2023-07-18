using Phonebook.Model;
using Spectre.Console;

namespace Phonebook;

internal class UserInterface
{
	internal static void DisplayContactTable(List<Contact> contacts)
	{
		var table = new Table();
		table.AddColumn("Id");
		table.AddColumn("Name");
		table.AddColumn("Phonenumber");

		foreach (var contact in contacts)
		{
			table.AddRow(contact.Id.ToString(), contact.Name, contact.PhoneNumber);
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
Phonenumber: {contact.PhoneNumber}");
		panel.Header = new PanelHeader("Contact info");
		panel.Padding = new Padding(2,2,2,2);

		AnsiConsole.Write(panel);

		Console.WriteLine("Press any key to continue");
		Console.ReadLine();
		Console.Clear();
	}
}
