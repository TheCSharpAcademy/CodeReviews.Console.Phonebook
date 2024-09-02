using FluentValidation;
using Spectre.Console;

namespace PhoneBook;

public class SpectreConsole
{
    private readonly ContactValidator _val;

    public SpectreConsole(ContactValidator validation)
    {
        _val = validation;
    }

    public void Welcome()
    {
        AnsiConsole.Write(
            new FigletText("PHONEBOOK")
                .Centered()
                .Color(Color.Green));
    }

    public void Goodbye()
    {
        AnsiConsole.WriteLine("Goodbye!");
    }

    public void Clear()
    {
        AnsiConsole.Clear();
    }

    public string Menu(List<string> contacts)
    {
        var list = new SelectionPrompt<string>()
            .EnableSearch()
            .AddChoices("Add Contact")
            .AddChoices("Exit")
            .AddChoices(contacts);
        
        return AnsiConsole.Prompt(list);
    }

    public string NewName()
    {
        var prompt = new TextPrompt<string>("Enter name:")
            .Validate(NameValidator)
            .ValidationErrorMessage("");
        return AnsiConsole.Prompt(prompt);
    }

    public string NewEmail()
    {
        var prompt = new TextPrompt<string>("Enter optional email:")
            .AllowEmpty()
            .Validate(EmailValidator)
            .ValidationErrorMessage("");
        return AnsiConsole.Prompt(prompt);
    }

    public string NewPhone()
    {
        var prompt = new TextPrompt<string>("Enter phone (10 digits):")
            .AllowEmpty()
            .Validate(PhoneValidator)
            .ValidationErrorMessage("");
        return AnsiConsole.Prompt(prompt);
    }

    public void ContactDetails(Contact contact)
    {
        var table = new Table();
        table.AddColumns("Key", "Value")
            .HideHeaders();

        table.AddRow("Name", contact.Name)
            .AddRow("Email", contact.Email)
            .AddRow("Phone", contact.Phone);
        
        AnsiConsole.Write(table);
    }

    public string ContactMenu()
    {
        var prompt = new SelectionPrompt<string>()
            .AddChoices("Edit")
            .AddChoices("Delete")
            .AddChoices("Return");
        
        return AnsiConsole.Prompt(prompt);
    }

    public string EditContactMenu()
    {
        var prompt = new SelectionPrompt<string>()
            .Title("Choose field to edit:")
            .AddChoices("Name", "Email", "Phone", "Return");
        
        return AnsiConsole.Prompt(prompt);
    }

    public bool ConfirmDelete(Contact contact)
    {
        var prompt = new ConfirmationPrompt($"Are you sure you want to remove {contact.Name}?");
        
        return AnsiConsole.Prompt(prompt);
    }

    public void Error(string message)
    {
        AnsiConsole.MarkupLine("[red bold]" + message + "[/]");
    }
    
    /********************************
     *** Custom Fluent Validators ***
     ********************************/

    private bool NameValidator(string name)
    {
        var res = _val.Validate(new Contact() { Name = name }, options =>
        {
            options.IncludeProperties(c => c.Name);
        });
        
        foreach (var error in res.Errors)
        {
            Error(error.ErrorMessage);
        }
        
        return res.IsValid;
    }

    private bool EmailValidator(string email)
    {
        var res = _val.Validate(new Contact() { Email = email }, options =>
        {
            options.IncludeProperties(c => c.Email);
        });

        foreach (var error in res.Errors)
        {
            Error(error.ErrorMessage);
        }
        
        return res.IsValid;
    }

    private bool PhoneValidator(string phone)
    {
        var res = _val.Validate(new Contact() { Phone = phone }, options =>
        {
            options.IncludeProperties(c => c.Phone);
        });

        foreach (var error in res.Errors)
        {
            Error(error.ErrorMessage);
        }
        
        return res.IsValid;
    }
}