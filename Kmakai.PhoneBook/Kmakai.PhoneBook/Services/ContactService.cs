
using Kmakai.PhoneBook.Controllers;
using Kmakai.PhoneBook.Models;
using Spectre.Console;

namespace Kmakai.PhoneBook.Services;

public class ContactService
{

    public static void CreateContact()
    {
        Contact contact = new Contact();

        var name = InputValidation.GetAndValidateName();

        var phoneNumber = InputValidation.GetAndValidateNumber();

        var email = InputValidation.GetAndValidateEmail();

        contact.Name = name;
        contact.PhoneNumber = phoneNumber;
        contact.Email = email;
        contact.CategoryId = CategoryController.GetCategoryIdByName();

        ContactController.AddContact(contact);

        AnsiConsole.MarkupLine($"[bold green]Contact {contact.Name} added successfully![/]");
        AnsiConsole.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public static void GetContact()
    {
        var contacts = ContactController.GetContacts();
        var contactNames = contacts.Select(x => x.Name).ToArray();
        var contactOption = AnsiConsole.Prompt<string>(new SelectionPrompt<string>()
                               .Title("Select contact")
                               .PageSize(10)
                               .MoreChoicesText("[grey](Move up and down to reveal more contacts)[/]")
                               .AddChoices(contactNames));

        var contact = contacts.SingleOrDefault(x => x.Name == contactOption);

        if (contact == null)
        {
            AnsiConsole.MarkupLine("[bold red]Contact not found![/]");
            return;
        }

        var table = new Table();

        table.AddColumn("Name");
        table.AddColumn("Phone Number");
        table.AddColumn("Email");
        table.AddColumn("Category");

        var category = CategoryController.GetCategoryIdById(contact.CategoryId);

        table.AddRow(contact.Name, contact.PhoneNumber, contact.Email, category?.Name ?? "Not Set");

        AnsiConsole.Write(table);
    }

    public static void UpdateContact()
    {
        var contacts = ContactController.GetContacts();
        var contactNames = contacts.Select(x => x.Name).ToArray();
        var contactOption = AnsiConsole.Prompt<string>(new SelectionPrompt<string>()
                                .Title("Select contact to update")
                                .PageSize(10)
                                .MoreChoicesText("[grey](Move up and down to reveal more contacts)[/]")
                                .AddChoices(contactNames));

        var contact = contacts.SingleOrDefault(x => x.Name == contactOption);
        if (contact == null)
        {
            AnsiConsole.MarkupLine("[bold red]Contact not found![/]");
            return;
        }

        contact.Name = AnsiConsole.Confirm("Update contact name?") ? InputValidation.GetAndValidateName() : contact.Name;
        contact.PhoneNumber = AnsiConsole.Confirm("Update contact phone number?") ? InputValidation.GetAndValidateNumber() : contact.PhoneNumber;
        contact.Email = AnsiConsole.Confirm("Update contact email?") ? InputValidation.GetAndValidateEmail() : contact.Email;
        contact.CategoryId = AnsiConsole.Confirm("Update contact category?") ? CategoryController.GetCategoryIdByName() : contact.CategoryId;

        ContactController.UpdateContact(contact);

        AnsiConsole.MarkupLine($"[bold green]Contact {contact.Name} updated successfully![/]");

        AnsiConsole.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public static void DeleteContact()
    {
        var contacts = ContactController.GetContacts();
        var contactNames = contacts.Select(x => x.Name).ToArray();
        var contactOption = AnsiConsole.Prompt<string>(new SelectionPrompt<string>()
                                .Title("Select contact to delete")
                                .PageSize(10)
                                .MoreChoicesText("[grey](Move up and down to reveal more contacts)[/]")
                                .AddChoices(contactNames));

        var contact = contacts.SingleOrDefault(x => x.Name == contactOption);
        if (contact == null)
        {
            AnsiConsole.MarkupLine("[bold red]Contact not found![/]");
            return;
        }

        ContactController.DeleteContactById(contact.Id);

        AnsiConsole.MarkupLine($"[bold green]Contact {contact.Name} deleted successfully![/]");

        AnsiConsole.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public static void GetContacts()
    {
        var contacts = ContactController.GetContacts();
        var categories = CategoryController.GetCategories();

        var table = new Table();

        table.AddColumn("Name");
        table.AddColumn("Phone Number");
        table.AddColumn("Email");
        table.AddColumn("Category");

        foreach (var contact in contacts)
        {
            var category = categories.SingleOrDefault(x => x.Id == contact.CategoryId);
            table.AddRow(contact.Name, contact.PhoneNumber, contact.Email, category?.Name ?? "Not set");
        }

        AnsiConsole.Write(table);

    }
}