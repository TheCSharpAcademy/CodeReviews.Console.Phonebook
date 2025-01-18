using Spectre.Console;

internal class ContactDelete
{
    internal static void Delete()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Deleting contact.[/]\n");

        var contact = ContactHelpers.GetContact();
        if (contact?.Name == null)
        {
            Console.Clear();
            return;
        }

        AnsiConsole.MarkupLine($"[red]You want to delete that contact:[/]");
        AnsiConsole.MarkupLine(
                $"[yellow]{contact.Name}[/] " +
                $"[yellow]{contact.Phone}[/] " +
                $"[yellow]{contact.Email}[/]");
        if (!DisplayInfoHelpers.ConfirmDeletion())
        {
            Console.Clear();
            return;
        }

        var deleteContact = new Action(() =>
        {
            using var database = new ContactContext();
            database.Contacts.Remove(contact);
            database.SaveChanges();
        });

        if (!ErrorHandler.Success(deleteContact)) return;
        AnsiConsole.MarkupLine($"Contact deleted successfully.");
        DisplayInfoHelpers.PressAnyKeyToContinue();
    }
}

