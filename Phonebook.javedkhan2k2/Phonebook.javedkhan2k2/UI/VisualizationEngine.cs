using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Phonebook.Entities;
using Spectre.Console;
namespace Drinks;

internal class VisualizationEngine
{

    internal static void DisplayContinueMessage()
    {
        AnsiConsole.Markup($"Press [blue]Enter[/] To Continue\n");
        Console.ReadLine();
    }

    public static Table CreateTable(string title, string footer)
    {
        var table = new Table();
        table.Title(title.ToUpper());
        table.Caption(footer);
        table.Border = TableBorder.Square;
        table.ShowRowSeparators = true;
        return table;
    }

    internal static void DisplayContacts(IEnumerable<Contact> contacts, [AllowNull] string title)
    {
        if (title == null)
        {
            title = "";
        }
        var table = CreateTable(title, $"Displaying [blue]{contacts.Count()}[/] records");
        // foreach(PropertyInfo property in contacts.FirstOrDefault().GetType().GetProperties())
        // {
        //     table.AddColumn(property.Name);
        // }
        table.AddColumns(["Id", "Name", "Email", "Phone Number"]);
        foreach (var contact in contacts)
        {
            table.AddRow(contact.Id.ToString(), contact.Name, contact.Email, contact.PhoneNumber);
        }
        AnsiConsole.Write(table);
    }
    
}