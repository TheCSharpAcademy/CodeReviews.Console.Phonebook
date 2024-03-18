using PhoneBook.Dejmenek.Models;
using Spectre.Console;

namespace PhoneBook.Dejmenek.Helpers;

public static class DataVisualizer
{
    public static void DisplayContacts(List<ContactDTO> contacts)
    {
        if (contacts.Count == 0)
        {
            AnsiConsole.MarkupLine("No contacts found.");
            return;
        }

        var table = new Table().Title("CONTACTS");

        table.AddColumn("Name");
        table.AddColumn("Category");
        table.AddColumn("Phone Number");
        table.AddColumn("Email");

        foreach (var contact in contacts)
        {
            table.AddRow(contact.Name, contact.CategoryName, contact.PhoneNumber, contact.Email);
        }

        AnsiConsole.Write(table);
    }

    public static void DisplayCategories(List<CategoryDTO> categories)
    {
        if (categories.Count == 0)
        {
            AnsiConsole.MarkupLine("No categories found.");
            return;
        }

        var table = new Table().Title("CATEGORIES");

        table.AddColumn("Name");

        foreach (var category in categories)
        {
            table.AddRow(category.Name);
        }

        AnsiConsole.Write(table);
    }
}
