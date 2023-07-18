using Phonebook.Model;
using Spectre.Console;

namespace Phonebook;

internal class UserInterface
{
	static internal void DisplayContactTable(List<Contact> contacts)
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
}
