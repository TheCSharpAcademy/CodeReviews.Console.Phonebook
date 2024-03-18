using Spectre.Console;

namespace PhoneBook;
internal class UserInterface
{
    internal static void ShowContact(Contact contact)
    {
        var panel = new Panel($@"
        Id: {contact.Id}
        Name: {contact.Name}
        Email: {contact.Email}
        PhoneNumber: {contact.PhoneNumber}
        ");
        panel.Header = new PanelHeader("Contact Info");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);

        Helper.waitUserToPressAnyKeyToContinue();
    }
    static internal void ShowContactTable(List<Contact> contacts)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Email");
        table.AddColumn("PhoneNumber");

        foreach (var contact in contacts)
        {
            table.AddRow(contact.Id.ToString(), contact.Name, contact.Email, contact.PhoneNumber);
        }

        AnsiConsole.Write(table);

        Helper.waitUserToPressAnyKeyToContinue();        
    }
}
