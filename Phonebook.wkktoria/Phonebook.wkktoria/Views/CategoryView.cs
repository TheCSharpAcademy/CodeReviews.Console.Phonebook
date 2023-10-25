using Phonebook.wkktoria.Models.Dtos;
using Spectre.Console;

namespace Phonebook.wkktoria.Views;

public static class CategoryView
{
    public static void ShowContactsInCategory(CategoryDto category, List<ContactDto> contacts)
    {
        var table = new Table();
        table.Title($"{category.Name}");

        table.AddColumn("Name");
        table.AddColumn("Email");
        table.AddColumn("Phone Number");

        foreach (var contact in contacts)
            table.AddRow(
                contact.Name!,
                contact.Email!,
                contact.PhoneNumber!
            );

        AnsiConsole.Write(table);
    }

    public static void ShowCategoriesTable(List<CategoryDto> categories)
    {
        var table = new Table();

        table.AddColumn("Name");
        table.AddColumn("Contacts");

        foreach (var category in categories) table.AddRow(category.Name!, category.Contacts!.Count.ToString());

        AnsiConsole.Write(table);
    }
}