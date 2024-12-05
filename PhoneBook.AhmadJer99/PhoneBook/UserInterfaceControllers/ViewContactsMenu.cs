using PhoneBook.Data;
using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.UserInterfaceControllers;

internal class ViewContactsMenu
{
    private enum ContactOperations
    {
        Edit,
        Delete,
        Done
    }

    private PhoneBookContext PhoneBookDb { get; set; }

    public ViewContactsMenu(PhoneBookContext phoneBookDb)
    {
        PhoneBookDb = phoneBookDb;
    }

    public async Task ListAllContactsAsync()
    {
        AnsiConsole.MarkupLine("[bold green]Contacts\n[/]");

        var contacts = PhoneBookDb.Contacts
            .OrderBy(x => x.ContactName).ToList();

        var backOption = new Contact { ContactName = "[gray]Back[/]" };
        var choices = contacts.Append(backOption).ToList();

        var selectedContact = AnsiConsole.Prompt(
            new SelectionPrompt<Contact>()
            .Title("[bold green]Select a contact to view its details\n[/]")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more contacts)[/]")
            .UseConverter(c => $"{c.ContactName}")
            .AddChoices(choices));

        if (selectedContact.ContactName == "[gray]Back[/]")
        { return; }
        FormattedViewContactDetails(selectedContact);
        await PromptOptionsAsync(selectedContact);
    }

    private void FormattedViewContactDetails(Contact selectedContact)
    {

        string phoneStatus = selectedContact.ContactPhoneStatus ? "[green]-Verified[/]" : "[red]-Unverified[/]";
        string emailStatus = selectedContact.ContactEmailStatus ? "[green]-Verified[/]" : "[red]-Unverified[/]";

        AnsiConsole.MarkupLine($"[bold teal]Name: [/]{selectedContact.ContactName}");
        AnsiConsole.MarkupLine($"[bold teal]Contact info:[/]");
        AnsiConsole.MarkupLine($"\t[bold teal]Phone: [/]{selectedContact.ContactPhone} {phoneStatus}");

        if (!string.IsNullOrEmpty(selectedContact.ContactEmail))
            AnsiConsole.MarkupLine($"\t[bold teal]Email: [/]{selectedContact.ContactEmail} {emailStatus}");
        if (!string.IsNullOrEmpty(selectedContact.ContactTitle))
            AnsiConsole.MarkupLine($"[bold teal]About {selectedContact.ContactName}: [/]{selectedContact.ContactTitle}\n");
    }

    public async Task PromptOptionsAsync(Contact selectedContact)
    {
        var userChoice = AnsiConsole.Prompt(
            new SelectionPrompt<ContactOperations>()
            .AddChoices(Enum.GetValues<ContactOperations>()));

        switch (userChoice)
        {
            case ContactOperations.Edit:
                await PromptEditAsync(selectedContact);
                break;
            case ContactOperations.Delete:
                await PromptDeleteAsync(selectedContact);
                break;
            case ContactOperations.Done:
                return;
        }
    }

    private async Task PromptEditAsync(Contact selectedContact)
    {
        while (true)
        {
            var editField = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .AddChoices(["Name", "Title", "Phone", "Email", "Cancel", "Save Changes"]));

            if (editField == "Cancel")
            {
                return;
            }
            if (editField == "Save Changes")
            {
                await PhoneBookDb.SaveChangesAsync();
                Console.Clear();
                FormattedViewContactDetails(selectedContact);
                IndicateSuccess();
                return;
            }
            var newValue = AnsiConsole.Ask<string>($"[bold white]Enter The New {editField}[/]:");
            switch (editField)
            {
                case "Name":
                    selectedContact.ContactName = newValue;
                    break;
                case "Title":
                    selectedContact.ContactTitle = newValue;
                    break;
                case "Phone":
                    selectedContact.ContactPhone = newValue;
                    break;
                case "Email":
                    selectedContact.ContactEmail = newValue;
                    break;
            }
        }
    }

    private async Task PromptDeleteAsync(Contact selectedContact)
    {
        var confirmation = AnsiConsole.Prompt(
            new ConfirmationPrompt("Any deleted contact can't be recovered are you sure?"));

        if (!confirmation)
        {
            AnsiConsole.MarkupLine("[red]Deletetion cancelled by user.[/]\n(Press Any Key To Continue)");
            Console.ReadKey();
            return;
        }
        PhoneBookDb.Remove(selectedContact);
        await PhoneBookDb.SaveChangesAsync();
        IndicateSuccess();

        Console.Clear();
        await ListAllContactsAsync();
    }

    private void IndicateSuccess()
    {
        AnsiConsole.MarkupLine("\n[green]Operation executed successfully![/]");
        AnsiConsole.MarkupLine("(Press Enter to continue)");
        Console.ReadLine(); 
    }
}