using Spectre.Console;

internal class ContactRead
{
    internal static void ShowAllContacts()
    {
        var contacts = new List<Contact>();
        var getContactsList = new Action(() =>
        {
            using var database = new ContactContext();
            contacts = database.Contacts.ToList();
        });

        if (!ErrorHandler.Success(getContactsList)) return;
        if (DisplayInfoHelpers.NoRecordsAvailable(contacts)) return;

        AnsiConsole.MarkupLine("List of contacts in database:\n");
        ShowContactsList(contacts);

        AnsiConsole.Markup("\n[yellow]Press any key to continue...[/] ");
        Console.ReadKey(true);
        Console.Clear();
    }

    internal static void ShowContactsList(List<Contact> contacts)
    {
        var table = new Table();
        int num = 1;
        table.AddColumn("[yellow]No.[/]");
        table.AddColumn("[yellow]Name[/]");
        table.AddColumn("[yellow]Phone[/]");
        table.AddColumn("[yellow]Email[/]");
        foreach (var contact in contacts)
        {
            table.AddRow(
                new Markup($"{num}"),
                new Markup($"{contact.Name}"),
                new Markup($"{contact.Phone}"),
                new Markup($"{contact.Email}"));
            num++;
        }
        AnsiConsole.Write(table);
    }
}
