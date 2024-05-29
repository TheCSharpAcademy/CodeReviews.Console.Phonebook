using Phonebook.Models;
using Phonebook.Repositories;
using Spectre.Console;

namespace Phonebook;
internal class ContactService
{
    internal static void InsertContact()
    {
        var contact = new Contact();

        var name = AnsiConsole.Ask<string>("Enter contact name (press 0 to cancel):");

        while (NameExists(name))
        {
            AnsiConsole.MarkupLineInterpolated($"\n[red]Contact with the name {name} already exists![/]");
            name = AnsiConsole.Ask<string>("Please enter a [blue]unique[/] contact name (press 0 to cancel):");
        }

        if (name == "0")
        {
            return;
        }

        contact.Name = name.Trim();

        AnsiConsole.MarkupLine("\n[blue]Format: firstname@domain.com[/]");
        var email = AnsiConsole.Ask<string>("Enter contact email adress (press 0 to cancel):");

        while (!Validate.IsValidEmail(email))
        {
            AnsiConsole.MarkupLine($"\n[red]Invalid email adress![/]");
            AnsiConsole.MarkupLine("[blue]Format: firstname@domain.com[/]");
            email = AnsiConsole.Ask<string>("Please enter valid email adress (press 0 to cancel):");
        }

        if (email == "0")
        {
            return;
        }

        contact.Email = email.Trim();

        AnsiConsole.MarkupLine("\n[blue]Format: 01/4444-555 | 01/44-55-66 | 042/333-999 | 0800/556677 | 1234567[/]");
        var phoneNumber = AnsiConsole.Ask<string>("Enter contact phone number (press 0 to cancel):");

        while (!Validate.IsValidPhoneNumber(phoneNumber))
        {
            AnsiConsole.MarkupLine($"\n[red]Invalid phone number![/]");
            AnsiConsole.MarkupLine("[blue]Format: 01/4444-555 | 01/44-55-66 | 042/333-999 | 0800/556677 | 1234567[/]");
            phoneNumber = AnsiConsole.Ask<string>("Enter contact phone number (press 0 to cancel):");
        }

        if (phoneNumber == "0")
        {
            return;
        }

        contact.PhoneNumber = phoneNumber.Trim();

        if (AnsiConsole.Confirm($"Add a new contact {contact.Name}?"))
        {
            ContactRepository.AddContact(contact);

            AnsiConsole.MarkupLine("\n[green]New contact was successfully added![/]");
            AnsiConsole.Write("Press any key to continue...");
            Console.ReadKey();
        }

    }

    internal static void DeleteContact()
    {
        var contact = GetContactInput();

        if (contact.Id == 0)
        {
            AnsiConsole.MarkupLine("\n[red]No contacts found![/]");
            AnsiConsole.Write("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        if (AnsiConsole.Confirm($"Are you sure you want to delete the contact {contact.Name}?"))
        {
            ContactRepository.DeleteContact(contact);

            AnsiConsole.MarkupLine("\n[green]Contact was successfully deleted![/]");
            AnsiConsole.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }

    internal static void UpdateContact()
    {
        var contact = GetContactInput();

        var originalName = contact.Name;

        if (contact.Id == 0)
        {
            AnsiConsole.MarkupLine("\n[red]No contacts found![/]");
            AnsiConsole.Write("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        TableVisualization.ShowContact(contact);

        if (AnsiConsole.Confirm("Update name?"))
        {
            var name = AnsiConsole.Ask<string>("Enter contacts new name (or press 0 to cancel):");

            while (NameExists(name))
            {
                AnsiConsole.MarkupLineInterpolated($"\n[red]Contact with the name {name} already exists![/]");
                name = AnsiConsole.Ask<string>("Please enter a [blue]unique[/] contact name:");
            }

            if (name == "0")
            {
                return;
            }

            contact.Name = name.Trim();
        }

        if (AnsiConsole.Confirm("Update email?"))
        {
            AnsiConsole.MarkupLine("\n[blue]Format: firstname@domain.com[/]");
            var email = AnsiConsole.Ask<string>("Enter contacts new email (or press 0 to cancel):");

            while (!Validate.IsValidEmail(email))
            {
                AnsiConsole.MarkupLine($"\n[red]Invalid email adress![/]");
                AnsiConsole.MarkupLine("[blue]Format: firstname@domain.com[/]");
                email = AnsiConsole.Ask<string>("Please enter valid email adress (press 0 to cancel):");
            }

            if (email == "0")
            {
                return;
            }

            contact.Email = email.Trim();
        }

        if (AnsiConsole.Confirm("Update phone number?"))
        {
            AnsiConsole.MarkupLine("\n[blue]Format: 01/4444-555 | 01/44-55-66 | 042/333-999 | 0800/556677 | 1234567[/]");
            var phoneNumber = AnsiConsole.Ask<string>("Enter contacts new phone number (press 0 to cancel):");

            while (!Validate.IsValidPhoneNumber(phoneNumber))
            {
                AnsiConsole.MarkupLine($"\n[red]Invalid phone number![/]");
                AnsiConsole.MarkupLine("[blue]Format: 01/4444-555 | 01/44-55-66 | 042/333-999 | 0800/556677 | 1234567[/]");
                phoneNumber = AnsiConsole.Ask<string>("Enter contact phone number (press 0 to cancel):");
            }

            if (phoneNumber == "0")
            {
                return;
            }

            contact.PhoneNumber = phoneNumber.Trim();
        }

        if (AnsiConsole.Confirm($"Are you sure you want to save changes to the contact {originalName}"))
        {
            ContactRepository.UpdateContact(contact);

            AnsiConsole.MarkupLine("\n[green]Contact was successfully updated![/]");
            AnsiConsole.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }

    internal static void GetContacts()
    {
        var contacts = ContactRepository.GetContacts();
        if (!contacts.Any())
        {
            AnsiConsole.MarkupLine("\n[red]No contacts found![/]");
            AnsiConsole.Write("Press any key to continue...");
            Console.ReadKey();
            return;
        }
        TableVisualization.ShowContacts(contacts);
    }

    internal static void GetContact()
    {
        var contact = GetContactInput();

        if (contact.Id == 0)
        {
            AnsiConsole.MarkupLine("\n[red]No contacts found![/]");
            AnsiConsole.Write("Press any key to continue...");
            Console.ReadKey();
            return;
        }
        TableVisualization.ShowContact(contact);

        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }

    private static Contact GetContactInput()
    {
        var contacts = ContactRepository.GetContacts();

        if (contacts.Any())
        {
            var contactNames = contacts.Select(x => x.Name).ToList();

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Select contact:")
                .AddChoices(contactNames)
                );

            var id = contacts.Find(x => x.Name == selection)?.Id ?? 0;

            var contact = ContactRepository.GetContactById(id);

            return contact ?? new Contact();
        }
        return new Contact();
    }

    private static bool NameExists(string name)
    {
        var contacts = ContactRepository.GetContacts();
        return contacts.Any(x => x.Name == name);
    }
}
