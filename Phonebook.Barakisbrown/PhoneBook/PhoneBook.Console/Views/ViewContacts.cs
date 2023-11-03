namespace PhoneBook.Console.Views;

using PhoneBook.Console.DataLayer;
using PhoneBook.Console.Model;
using Spectre.Console;

public class ViewContacts
{
    private readonly PhoneContext _dbContext;

    public ViewContacts(PhoneContext _context)
    {
        _dbContext = _context;
    }

    public void displayContacts()
    {
        AnsiConsole.Clear();
        var table = new Table();
        table.Border = TableBorder.Rounded;
        table.Title = new TableTitle($"All {_dbContext.Contacts.Count()} Contacts Listed");
        // COLUMNS
        table.AddColumn(new TableColumn("Full Name").Centered());
        table.AddColumn(new TableColumn("Email").Centered());
        table.AddColumn(new TableColumn("Phone Number").Centered());
        table.Centered();
        // END COLUMNS

        // ROWS
        List<Contact> contacts = _dbContext.GetContacts();

        foreach (var contact in contacts)
        {
            table.AddRow(contact.Name, contact.Email, contact.PhoneNumber);
        }
        // End Rows
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
        AnsiConsole.Markup("\t\t\t\t[slowblink]Press any key to return to the main menu.[/]");
        System.Console.ReadKey(true);
        Thread.Sleep(1000);
    }
}