using Phonebook.Models;
using Spectre.Console;

namespace Phonebook;
internal static class TableVisualization
{
    internal static void ShowContacts(List<Contact> contacts)
    {
        var table = new Table();
        table.AddColumns("[blue]Name[/]", "[blue]Email[/]", "[blue]Phone Number[/]");

        foreach (var contact in contacts)
        {
            table.AddRow(contact.Name, contact.Email, contact.PhoneNumber);
        }

        AnsiConsole.Write(table);
        AnsiConsole.Write("Press any key to continue...");
        Console.ReadKey();
    }

    internal static void ShowContact(Contact contact)
    {
        var panel = new Panel($@"[blue]Name:[/] {contact.Name}
[blue]Email:[/] {contact.Email}
[blue]Phone:[/] {contact.PhoneNumber}");
        panel.Header = new PanelHeader("Contact info");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);
    }
}
