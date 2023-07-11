using Spectre.Console;

namespace LONCHANICK_PhoneBookConsoleApp;

internal class ContactsUI
{
	//This method just shows a table with all contacts, it does not return any option
	public static void ShowAllContacts(List<Contact> cont)
	{
		var table = new Table();
		table.AddColumn("Id");
		table.AddColumn("Name");
		table.AddColumn("Phone Number");

		foreach (var c in cont)
			table.AddRow(c.Id.ToString(), c.Name, c.PhoneNumber);

		AnsiConsole.Write(table);
		Console.WriteLine("Press any key to continue ...");
		Console.ReadLine();
	}

	public static Contact ShooseAnOptionCONTACTS()
	{
		var contacts = ContactRepository.GetContacts();
		var options = contacts.Select(x => x.Name).ToList();
		var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
			.Title("Choose any Contact")
			.AddChoices(options));
		var idOption = contacts.First(x => x.Name == option).Id;
		return ContactRepository.GetContactById(idOption);
	}

	internal static void ShowSingleContactDetails(Contact c)
	{
		var panel = new Panel($@"Id: {c.Id}
Name: {c.Name}
Phone Number: {c.PhoneNumber}
");
		panel.Header = new PanelHeader("Contact Info");
		panel.Padding = new Padding(1, 1, 1, 1);
		AnsiConsole.Write(panel);
		Console.WriteLine("Press any key to continue ...");
		Console.ReadLine();
	}
}

