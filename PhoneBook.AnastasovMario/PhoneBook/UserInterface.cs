using PhoneBook.Data.Models;
using Spectre.Console;

namespace PhoneBook
{
	static public class UserInterface
	{
		public static void ShowContacts(List<Contact> contacts)
		{
      var table = new Table();
      table.AddColumn("Id");
      table.AddColumn("Name");
      table.AddColumn("Email");
      table.AddColumn("Phone");

      foreach (var contact in contacts)
      {
        table.AddRow(contact.Id.ToString(), contact.Name, contact.Email, contact.PhoneNumber);
      }

      AnsiConsole.Write(table);
    }

    public static void ShowContact(Contact contact)
    {
      var table = new Table();
      table.AddColumn("Id");
      table.AddColumn("Name");
      table.AddColumn("Email");
      table.AddColumn("Phone");

      table.AddRow(contact.Id.ToString(), contact.Name, contact.Email, contact.PhoneNumber);

      AnsiConsole.Write(table);
    }
	}
}
