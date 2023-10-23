using Phonebook.wkktoria.Models.Dtos;
using Spectre.Console;

namespace Phonebook.wkktoria.Views;

public static class ContactView
{
    public static void ShowContactDetails(ContactDto contact)
    {
        var panel = new Panel($"""
                               Name: {contact.Name}
                               Email Address: {contact.Email}
                               Phone Number: {contact.PhoneNumber}
                               Category: {contact.Category.Name}
                               """)
        {
            Header = new PanelHeader("Contact Details"),
            Padding = new Padding(1)
        };

        AnsiConsole.Write(panel);
    }

    public static void ShowContactsTable(List<ContactDto> contacts)
    {
        var table = new Table();

        table.AddColumn("Name");
        table.AddColumn("Email");
        table.AddColumn("Phone Number");
        table.AddColumn("Category");

        foreach (var contact in contacts)
            table.AddRow(
                contact.Name,
                contact.Email,
                contact.PhoneNumber,
                contact.Category.Name
            );

        AnsiConsole.Write(table);
    }
}