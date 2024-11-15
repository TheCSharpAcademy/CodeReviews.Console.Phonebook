using Spectre.Console;

namespace PhoneBook.mefdev.Controllers;

internal abstract class BaseController
{
    protected void DisplayMessage(string message, string color = "yellow")
    {
        AnsiConsole.MarkupLine($"[{color}]{message}[/]");
    }

    protected bool ConfirmDeletion(string itemName)
    {
        var confirm = AnsiConsole.Confirm($"Are you sure you want to delete [red]{itemName}[/]?");

        return confirm;
    }

    protected void RenderCustomLine(string color, string title)
    {
        var rule = new Rule($"[{color}]{title}[/]");
        rule.RuleStyle($"{color} dim");
        AnsiConsole.Write(rule);
    }

    protected string GetName(string prevName = "")
    {
        return AnsiConsole.Ask<string>($"Enter name ({prevName}): ");
    }

    protected string GetPhoneNumber(string prevPhone = "")
    {
        var phoneNumber = "";
        do
        {
            phoneNumber = AnsiConsole.Ask<string>($"""
                                                    [lightgoldenrod2]Supported formats for phone numbers:
                                                        - (+CountryCode AreaCode LocalNumber): (+1 123 456 7890)
                                                        - (AreaCode LocalNumber): ((123) 456-7890)
                                                        - (Standard): (1234567890)[/]
                                                    Please enter phone number({prevPhone}): 
                                                    """);
        } while (!Validators.IsValidPhone(phoneNumber));
        return phoneNumber;
    }

    protected string GetEmail(string prevEmail = "")
    {
        var email = "";
        do
        {
            email = AnsiConsole.Ask<string>($"""
                                                    [lightgoldenrod2]Supported formats for Email:
                                                        - username@domain.com
                                                        Example: example@domain.com[/]
                                                    Please enter Email ({prevEmail}):
                                                    """);
        } while (!Validators.IsValidEmail(email));
        return email;
    }

    protected void DisplayItemTable<T>(T item)
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.DodgerBlue3)
            .Width(150);

        var properties = item.GetType().GetProperties();

        foreach (var prop in properties)
        {
            table.AddColumn(new TableColumn(prop.Name));
        }
        var rowValues = properties.Select(prop => prop.GetValue(item)?.ToString() ?? "N/A").ToArray();
        table.AddRow(rowValues);

        AnsiConsole.Write(table);
        AnsiConsole.Confirm("Press any key to continue... ");
    }

    protected void DisplayAllItems<T>(List<T> items)
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.DodgerBlue3)
            .Width(150);

        var firstItem = items.FirstOrDefault();
        if (firstItem != null)
        {
            foreach (var prop in firstItem.GetType().GetProperties())
            {
                table.AddColumn(new TableColumn(prop.Name));
            }
        }
        if(items != null && items.Count() > 0)
        {
            foreach (var item in items)
            {
                var rowValues = item.GetType().GetProperties()
                    .Select(prop => prop.GetValue(item)?.ToString() ?? "N/A")
                    .ToArray();

                table.AddRow(rowValues);
            }
        }
        AnsiConsole.Write(table);
        AnsiConsole.Confirm("Press any key to continue... ");
    }
}

