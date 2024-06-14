using System.Diagnostics.CodeAnalysis;
using Phonebook.Entities;
using Spectre.Console;
namespace Phonebook;

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
        table.AddColumns(["Id", "Name", "Email", "Phone Number", "Category"]);
        foreach (var contact in contacts)
        {
            table.AddRow(contact.Id.ToString(), contact.Name, contact.Email, contact.PhoneNumber, contact.ContactCategory.CategoryName);
        }
        AnsiConsole.Write(table);
    }

    internal static void DisplayContactCategoriess(IEnumerable<ContactCategory> contactcategories, string title)
    {
        if (title == null)
        {
            title = "";
        }
        var table = CreateTable(title, $"Displaying [blue]{contactcategories.Count()}[/] records");
        table.AddColumns(["Id", "Category Name"]);
        foreach (var contactCategory in contactcategories)
        {
            table.AddRow(contactCategory.Id.ToString(), contactCategory.CategoryName);
        }
        AnsiConsole.Write(table);
    }

    internal static void DisplayContactDetail(Contact contact, string title)
    {
        if (title == null)
        {
            title = "";
        }
        var table = CreateTable(title, $"Displaying [blue]Contact Details[/]");
        table.AddColumns([" ", " "]);
        table.AddRow("Id", contact.Id.ToString());
        table.AddRow("Name", contact.Name);
        table.AddRow("Category", contact.ContactCategory.CategoryName);
        table.AddRow("Email", contact.Email);
        table.AddRow("Phone Number", contact.PhoneNumber);
        AnsiConsole.Write(table);
    }
}