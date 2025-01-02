using Spectre.Console;

namespace PhoneBook.Bina28;

internal class UserInterface
{
	internal static void ShowContact(PhoneBook phoneBook)
	{
		var panel = new Panel($@"Id: {phoneBook.Id}
Name: {phoneBook.Name}
Phone Number: {phoneBook.PhoneNumber}
Email: {phoneBook.Email}");
		panel.Header = new PanelHeader("Contact Information");
		panel.Padding = new Padding(2, 2, 2, 2);

		AnsiConsole.Write(panel);

		Console.WriteLine("Press any key to continue");
		Console.ReadLine();
		Console.Clear();

	}

	static internal void ShowPhonebookTable(List<PhoneBook> phoneBook)
	{
		var table = new Table();
		table.AddColumn("Id");
		table.AddColumn("Name");
		table.AddColumn("Phone number");
		table.AddColumn("Email");

		foreach (var data in phoneBook)
		{
			table.AddRow(data.Id.ToString(), data.Name, data.PhoneNumber, data.Email);
		}

		AnsiConsole.Write(table);

		Console.WriteLine("Press any key to continue");
		Console.ReadLine();
		Console.Clear();
	}
}
