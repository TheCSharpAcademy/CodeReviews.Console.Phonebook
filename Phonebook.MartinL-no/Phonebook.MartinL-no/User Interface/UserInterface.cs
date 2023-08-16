using Spectre.Console;

namespace Phonebook.MartinL_no.UserInterface;

static internal class UserInterface
{
	static public void ShowMenu()
	{
        while (true)
        {
		    var option = AnsiConsole.Prompt(
			    new SelectionPrompt<MenuOptions>()
			    .Title("What would you like to do:")
			    .AddChoices(
				    MenuOptions.AddContact,
				    MenuOptions.DeleteContact,
				    MenuOptions.UpdateContact,
				    MenuOptions.ViewAllContacts,
				    MenuOptions.ViewContact));

		    switch (option)
		    {
			    case MenuOptions.AddContact:
				    AddContact();
                    break;
			    case MenuOptions.DeleteContact:
				    DeleteContact();
                    break;
                case MenuOptions.UpdateContact:
				    UpdateContact();
                    break;
                case MenuOptions.ViewAllContacts:
				    ViewAllContacts();
                    break;
                case MenuOptions.ViewContact:
				    ViewContact();
                    break;
            }
        }
	}

    private static void ViewContact()
    {
        throw new NotImplementedException();
    }

    private static void ViewAllContacts()
    {
        var contacts = ContactController.GetContacts();

        ShowContactTable(contacts);
    }

    private static void UpdateContact()
    {
        throw new NotImplementedException();
    }

    private static void DeleteContact()
    {
        throw new NotImplementedException();
    }

    private static void AddContact()
    {
        throw new NotImplementedException();
    }

    private static void ShowContactTable(List<Contact> contacts)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Phone Number");

        foreach (var contact in contacts)
        {
            table.AddRow(contact.Id.ToString(), contact.Name, contact.PhoneNumber);
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Enter any key to continue");
        Console.ReadKey();
        Console.Clear();
    }
}

