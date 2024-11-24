using System;
using System.Text.RegularExpressions;
using Spectre.Console;
using TwilightSaw.Phonebook.Model;

namespace TwilightSaw.Phonebook.Helpers;

public class UserInput
{
    public static string CreateRegex(string regexString, string messageStart, string messageError)
    {
        Regex regex = new Regex(regexString);
        var input = AnsiConsole.Prompt(
            new TextPrompt<string>($"[green]{messageStart} or 0 to exit:[/]")
                .Validate(value => regex.IsMatch(value)
                    ? ValidationResult.Success()
                    : ValidationResult.Error($"[red]{messageError}[/]")));
        Console.Clear();
        return input;
    }

    public static string Create(string messageStart)
    {
        var input = AnsiConsole.Prompt(
            new TextPrompt<string>($"[green]{messageStart} or 0 to exit: [/]"));
        Console.Clear();
        return input;
    }

    public static string CreateChoosingList(List<string> variants, string backVariant)
    {
        variants.Add(backVariant);
        return AnsiConsole.Prompt(new SelectionPrompt<string>()
           .Title("[blue]Please, choose an option from the list below:[/]")
           .PageSize(10)
           .MoreChoicesText("[grey](Move up and down to reveal more categories[/]")
           .AddChoices(variants)); 
    }

    public static Contact CreateContactChoosingList(List<Contact> variants, string backVariant, string categoryName)
    {
        variants.Add(new Contact("Add new contact", null, null));
        if (categoryName != "All contacts")
        {
            variants.Add(new Contact("Change the name of this category", null, null));
            variants.Add(new Contact("Delete this category", null, null));
        }
        variants.Add(new Contact(backVariant, null, null));
        return AnsiConsole.Prompt(new SelectionPrompt<Contact>()
            .Title("[blue]Please, choose an option from the list below:[/]")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more categories[/]")
            .AddChoices(variants));
    }

    public static Category CreateCategoryChoosingList(List<Category> variants, string backVariant)
    {   
        variants.Add(new Category("All contacts"));
        variants.Add(new Category("Add new category"));
        variants.Add(new Category(backVariant));
        return AnsiConsole.Prompt(new SelectionPrompt<Category>()
            .Title("[blue]Please, choose an option from the list below:[/]")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more categories[/]")
            .AddChoices(variants));
    }

    public static Contact CreateContact()
    {
        var name = Create("Insert a name of the contact");
        if (name == "0") return null;
        var phone = CreateRegex(@"^\+[0-9]\d{10,15}$|^0$", "Insert a phone number of the contact(example: +380671236534)", "Wrong format.");
        if (phone == "0") return null;
        var email = CreateRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$|^0$", "Insert an email of the contact(example: test@gmail.com)", "Wrong format.");
        if (email == "0") return null;
        return new Contact(name, email, phone);
    }

    public static string UpdateContact(string value)
    {
        var attribute = "";
        switch (value)
        {
            case "Name":
                attribute = Create("Insert a new name of the contact");
                if (attribute == "0") return null;
                break;
            case "Phone Number":
                attribute = CreateRegex(@"^\+[0-9]\d{10,15}$|^0$", "Insert a new phone number of the contact(example: +380671236534)", "Wrong format.");
                if (attribute == "0") return null;
                break;
            case "Email":
                attribute = CreateRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$|^0$", "Insert an new email of the contact(example: test@gmail.com)", "Wrong format.");
                if (attribute == "0") return null;
                break;
        }
        return attribute;
    }
}