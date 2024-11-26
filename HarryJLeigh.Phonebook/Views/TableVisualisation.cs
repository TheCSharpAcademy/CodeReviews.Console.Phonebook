using Phonebook.Models;
using Spectre.Console;

namespace Phonebook.Views;

public static class TableVisualisation
{
    internal static void ShowTable(List<Contact> contacts)
    {
        var table = new Table().AddColumns("ID", "Name", "Email", "Number", "Category");
        foreach (Contact contact in contacts)
        {
            table.AddRow(
                contact.Id.ToString(),
                contact.Name,
                contact.Email,
                contact.PhoneNumber,
                contact.Category
            );
        }
        AnsiConsole.Write(table);
    }
}