using Spectre.Console;

internal class ContactUpdate
{
    internal static void Update()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Updating contact:[/]");

        var contact = ContactHelpers.GetContact();
        if (contact?.Name == null)
        {
            Console.Clear();
            return;
        }

        AnsiConsole.MarkupLine(
            $"Name: [yellow]{contact.Name}[/] | " +
            $"Phone: [yellow]{contact.Phone}[/] | " +
            $"Email: [yellow]{contact.Email}[/]\n");

        var (name, phone, email) = ContactCreate.GetContactInput();
        contact.Name = name;
        contact.Phone = phone;
        contact.Email = email;

        var updateContact = new Action(() =>
        {
            using var database = new ContactContext();
            database.Update(contact);
            database.SaveChanges();
        });

        if (!ErrorHandler.Success(updateContact)) return;
        AnsiConsole.MarkupLine($"Contact updated successfully.");
        DisplayInfoHelpers.PressAnyKeyToContinue();
    }
}
