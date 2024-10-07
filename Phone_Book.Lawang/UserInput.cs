using Phone_Book.Lawang.Models;
using Phone_Book.Lawang.View;
using Spectre.Console;

namespace Phone_Book.Lawang;

public class UserInput
{
    public Option ChooseOperation()
    {
        var listOfOption = new List<Option>()
        {
            new Option() {Display = "Create Contact", Value = 1},
            new Option() {Display = "Get all Contacts", Value = 2},
            new Option() {Display = "Update Contact", Value = 3},
            new Option() {Display = "Delete Contact", Value = 4},
            new Option() {Display = "Send Email", Value = 5},
            new Option() {Display = "Send SMS", Value = 6},
            new Option() {Display = "Exit", Value = 0}

        };

        return listOptions(listOfOption, "[blue bold]Choose your operation[/]", "What do you want to do?");
    }

    public int ChooseId(IEnumerable<Contact> contacts)
    {

        AnsiConsole.Write(new Rule($"Choose the Correct Id given in above table.").LeftJustified().RuleStyle("red"));
        AnsiConsole.MarkupLine("\n[grey bold](Press '0' to go back to menu.)\n[/]");
        var id = AnsiConsole.Ask<int>("[green bold]Enter the Id: [/]");

        while (!Validation.IsIdValid(contacts, id))
        {
            if (id == 0) return 0;
            AnsiConsole.MarkupLine("[red bold]The element of Input Id doesn't exist, Please give the correct Id from the table![/]");
            id = AnsiConsole.Ask<int>("[green bold]Enter the Id: [/]");
        }

        return id;
    }
    public Option ChooseUpdateOption()
    {
        var listOfUpdateOption = new List<Option>()
        {
            new Option() {Display = "Update Name", Value = 1},
            new Option() {Display = "Update Email", Value = 2},
            new Option() {Display = "Update Phone Number", Value = 3},
            new Option() {Display = "Update Entire Contact", Value = 4},
            new Option() {Display = "Exit", Value = 0}
        };

        return listOptions(listOfUpdateOption, "Choose the [yellow]\"UPDATE\"[/] operation", "What operation do you want to perform?");
    }
    private Option listOptions(List<Option> options, string heading, string title)
    {
        AnsiConsole.Write(new Rule($"{heading}").LeftJustified().RuleStyle("red"));
        Console.WriteLine();

        var selection = AnsiConsole.Prompt(new SelectionPrompt<Option>()
            .Title($"{title}")
            .UseConverter<Option>(o => o.Display)
            .MoreChoicesText("[grey bold](Press 'up' and 'down' key to navigate)[/]")
            .AddChoices(options)
            .HighlightStyle(Color.Blue3)
            .WrapAround()
        );

        return selection;
    }

    public string GetSelection(List<string> categories, string heading, string title)
    {
        AnsiConsole.Write(new Rule($"{heading}").LeftJustified().RuleStyle("red"));
        Console.WriteLine();

        var selection = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title($"{title}")
            .MoreChoicesText("[grey bold](Press 'up' and 'down' key to navigate)[/]")
            .AddChoices(categories)
            .HighlightStyle(Color.Blue3)
            .WrapAround()
        );

        return selection;
    }

    public Contact? CreateContact()
    {
        Console.Clear();
        Visual.ShowOperationTitle("[aqua bold]CREATE CONTACT[/]");
        AnsiConsole.MarkupLine("\n[grey bold](press '0' to go back to menu)\n[/]");
        var name = AnsiConsole.Ask<string>("[green bold]Write Name of the Contact: [/]");
        if (name == "0") return null;

        var email = AnsiConsole.Ask<string>("[green bold]Email Address: [/]");

        while (!Validation.IsEmailValid(email ?? ""))
        {
            if (email == "0") return null;
            AnsiConsole.Markup("[red bold]Invalid Input, Try Again (eg:- abc@gmail.com): [/]");
            email = Console.ReadLine()?.Trim();
        }
        var countryCode = AnsiConsole.Ask<string>("[green bold]Enter Country Code with '+' at the begining: [/]");
        while (!Validation.IsCountryCodeValid(countryCode ?? ""))
        {
            if (countryCode == "0") return null;
            AnsiConsole.Markup("[red bold]Invalid Input, Include '+' at the start (eg:- +91): [/]");
            countryCode = Console.ReadLine()?.Trim();
        }
        var number = AnsiConsole.Ask<string>("[green bold]Phone Number: [/]");
        while (!Validation.IsNumberValid(number ?? ""))
        {
            if (number == "0") return null;
            AnsiConsole.Markup("[red bold]Invalid Input, Include 10 digit at the start (eg:- 1234567890): [/]");
            number = Console.ReadLine()?.Trim();
        }

        return new Contact() { Name = name, Email = email, PhoneNumber = countryCode + number };
    }

    public Contact? UpdateContact(Contact updatedContact, Option updateOption)
    {
        switch (updateOption.Value)
        {
            case 1:
                updatedContact.Name = AnsiConsole.Ask<string>("Write the Updated Name: ");
                break;
            case 2:
                var email = AnsiConsole.Ask<string>("[green bold]Email Address: [/]");

                while (!Validation.IsEmailValid(email ?? ""))
                {
                    AnsiConsole.Markup("[red bold]Invalid Input, Try Again (eg:- abc@gmail.com): [/]");
                    email = Console.ReadLine()?.Trim();
                }
                updatedContact.Email = email;
                break;

            case 3:
                var countryCode = AnsiConsole.Ask<string>("[green bold]Enter Country Code with '+' at the begining: [/]");
                while (!Validation.IsCountryCodeValid(countryCode ?? ""))
                {
                    if (countryCode == "0") return null;
                    AnsiConsole.Markup("[red bold]Invalid Input, Include '+' at the start (eg:- +91): [/]");
                    countryCode = Console.ReadLine()?.Trim();
                }
                var number = AnsiConsole.Ask<string>("[green bold]Phone Number: [/]");
                while (!Validation.IsNumberValid(number ?? ""))
                {
                    if (number == "0") return null;
                    AnsiConsole.Markup("[red bold]Invalid Input, Include 10 -14 digit of your number (eg:- 1234567890): [/]");
                    number = Console.ReadLine()?.Trim();
                }
                updatedContact.PhoneNumber = countryCode + number;
                break;

            case 4:
                Visual.ShowOperationTitle("[aqua bold]UPDATE CONTACT[/]");
                AnsiConsole.MarkupLine("\n[grey bold](press '0' to go back to menu)\n[/]");
                var name = AnsiConsole.Ask<string>("[green bold]Write Name of the Contact: [/]");

                email = AnsiConsole.Ask<string>("[green bold]Email Address: [/]");

                while (!Validation.IsEmailValid(email ?? ""))
                {
                    AnsiConsole.Markup("[red bold]Invalid Input, Try Again (eg:- abc@gmail.com): [/]");
                    email = Console.ReadLine()?.Trim();
                }

                number = AnsiConsole.Ask<string>("[green bold]Phone Number: [/]");
                while (!Validation.IsNumberValid(number ?? ""))
                {
                    AnsiConsole.Markup("[red bold]Invalid Input, Include 10 digit and '+' at the start (eg:- +1234567890): [/]");
                    number = Console.ReadLine()?.Trim();
                }
                updatedContact.Name = name;
                updatedContact.Email = email;
                updatedContact.PhoneNumber = number;
                break;

            case 0:
                return null;
        }

        return updatedContact;
    }

    public string CreateCategory()
    {
        return AnsiConsole.Ask<string>("Enter New Category: ");
    }
}
