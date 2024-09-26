using Spectre.Console;
using Phonebook.Library.Models;

namespace Phonebook.Library;

public class PresentationLayer
{
    public static void ShowTable(List<Contact> contactList)
    {
        var table = new Table();
        table.Title = new TableTitle("Phonebook", style: "underline yellow");
        string[] columns = ["Id", "Name", "Email", "Phone number", "Category"];
        table.AddColumns(columns);

        foreach (Contact contact in contactList)
        {
            table.AddRow(contact.Id.ToString(), contact.Name, contact.Email, contact.PhoneNumber.ToString(), contact.Category);
        }
        AnsiConsole.Write(table);
    }
}
