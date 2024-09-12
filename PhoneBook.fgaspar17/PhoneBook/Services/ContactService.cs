using PhoneBookLibrary;
using Spectre.Console;

namespace PhoneBook;

public static class ContactService
{
    public static void CreateContact()
    {
        MenuPresentation.PresentMenu("[blue]Inserting[/]");
        bool isCancelled;
        string name, email, phoneNumber;

        UniquePropertyValidator<string, Contact> uniqueContact = new()
        {
            ErrorMsg = "Name must be unique.",
            GetModel = ContactController.GetContactByName
        };

        (isCancelled, name) = AskForContactName(uniqueContact);
        if (isCancelled) return;

        (isCancelled, email) = AskForContactEmail();
        if (isCancelled) return;

        (isCancelled, phoneNumber) = AskForContactPhoneNumber();
        if (isCancelled) return;

        ContactController.InsertContact(new Contact { Name = name, Email = email, PhoneNumber = phoneNumber });
    }

    public static void UpdateContact()
    {
        MenuPresentation.PresentMenu("[yellow]Updating[/]");
        bool isCancelled;
        string oldName, newName, email, phoneNumber;

        ShowContactTable();

        AnsiConsole.WriteLine("Current Name");
        ExistingModelValidator<string, Contact> existingContact = new()
        {
            ErrorMsg = "Contact Name doesn't exist.",
            GetModel = ContactController.GetContactByName
        };

        (isCancelled, oldName) = AskForContactName(existingContact);
        if (isCancelled) return;

        AnsiConsole.WriteLine("New Name");
        UniquePropertyValidator<string, Contact> uniqueContact = new()
        {
            ErrorMsg = "Name must be unique.",
            GetModel = ContactController.GetContactByName,
            PropertyName = "Name",
            ExcludedValues = [(object)oldName]
        };
        (isCancelled, newName) = AskForContactName(uniqueContact);
        if (isCancelled) return;

        (isCancelled, email) = AskForContactEmail();
        if (isCancelled) return;

        (isCancelled, phoneNumber) = AskForContactPhoneNumber();
        if (isCancelled) return;

        ContactController.UpdateContact(new Contact { Id = ContactController.GetContactByName(oldName).Id, Name = newName, Email = email, PhoneNumber = phoneNumber });
    }

    public static void DeleteContact()
    {
        MenuPresentation.PresentMenu("[red]Deleting[/]");
        bool isCancelled;
        string name;

        ShowContactTable();

        ExistingModelValidator<string, Contact> existingContact = new ExistingModelValidator<string, Contact>
        {
            ErrorMsg = "Contact Name doesn't exist.",
            GetModel = ContactController.GetContactByName
        };

        (isCancelled, name) = AskForContactName(existingContact);
        if (isCancelled) return;

        ContactController.DeleteContact(new Contact { Id = ContactController.GetContactByName(name).Id });
    }

    public static void ShowContact()
    {
        bool isCancelled;
        string name;

        ExistingModelValidator<string, Contact> existingContact = new ExistingModelValidator<string, Contact>
        {
            ErrorMsg = "Contact Name doesn't exist.",
            GetModel = ContactController.GetContactByName
        };

        (isCancelled, name) = AskForContactName(existingContact);
        if (isCancelled) return;

        ContactDto contact = ContactMapper.MapToDto(ContactController.GetContactByName(name));
        if (contact != null)
            OutputRenderer.ShowPanel(contact, "Contact");
        else
            AnsiConsole.MarkupLine($"[red]Contact {name} not found[/]");
        Prompter.PressKeyToContinuePrompt();
    }

    public static void ShowContacts()
    {
        ShowContactTable();
        Prompter.PressKeyToContinuePrompt();
    }

    public static (bool IsCancelled, string Result) AskForContactName(params IValidator[] validators)
    {
        string message = "Enter a Contact Name";
        return Prompter.PromptWithValidation(message, validations: validators);
    }

    private static (bool IsCancelled, string Result) AskForContactEmail()
    {
        IValidator validator = new EmailValidator();
        string message = "Enter a Contact Email (e.g., user@example.com)";
        return Prompter.PromptWithValidation(message, validations: validator);
    }

    private static (bool IsCancelled, string Result) AskForContactPhoneNumber()
    {
        IValidator validator = new PhoneNumberValidator();
        string message = "Enter a Contact Phone Number (e.g., +11122233444)";
        return Prompter.PromptWithValidation(message, validations: validator);
    }

    public static void ShowContactTable()
    {
        List<ContactDto> contacts = ContactController.GetContacts().Select(c => ContactMapper.MapToDto(c)).ToList();
        OutputRenderer.ShowTable(contacts, "Contacts");
    }
}