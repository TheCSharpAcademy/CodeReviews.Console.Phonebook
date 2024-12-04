using Phonebook.Models;
using Spectre.Console;

namespace Phonebook;

public class DisplayData
{
    internal void ShowContacts(List<Contact> contacts)
    {
        var table = new Table();
        bool isAlternateRow = false;

        table.BorderColor(Color.DarkSlateGray1);
        table.Border(TableBorder.Rounded);
        table.Title("Contacts");
        table.AddColumn(new TableColumn("[cyan1]ID[/]").LeftAligned());
        table.AddColumn(new TableColumn("[green1]Name[/]").RightAligned());
        table.AddColumn(new TableColumn("[blue1]Phone Number[/]").RightAligned());
        table.AddColumn(new TableColumn("[yellow1]Email[/]").RightAligned());
        table.AddColumn(new TableColumn("[red1]Category[/]").LeftAligned());

        foreach (Contact contact in contacts)
        {
            var color = isAlternateRow ? "grey" : "blue";
            table.AddRow(
                $"[{color}]{contact.ContactId}[/]",
                $"[{color}]{contact.Name}[/]",
                $"[{color}]{contact.PhoneNumber}[/]",
                $"[{color}]{contact.Email}[/]",
                $"[{color}]{contact.Category.Name}[/]"
            );
            isAlternateRow = !isAlternateRow;
        }
        Console.Clear();
        AnsiConsole.Write(table);
    }

    internal void ShowCategories(List<Category> categories)
    {
        var table = new Table();
        bool isAlternateRow = false;

        table.BorderColor(Color.DarkSlateGray1);
        table.Border(TableBorder.Rounded);
        table.Title("Categories");
        table.AddColumn(new TableColumn("[cyan1]Category[/]").LeftAligned());
        table.AddColumn(new TableColumn("[green1]Name[/]").RightAligned());
        table.AddColumn(new TableColumn("[blue1]Phone Number[/]").RightAligned());
        table.AddColumn(new TableColumn("[yellow1]Email[/]").RightAligned());
        // table.AddColumn(new TableColumn("[red1]Category[/]").LeftAligned());

        var rows = categories.SelectMany(category =>
                category.Contacts.Any()
                ? category.Contacts.Select(contact =>
                    new[] { category.Name, contact.Name, contact.PhoneNumber, contact.Email })
                : new[]
                    { new[] { category.Name, "--", "--", "--" } });

        foreach (var row in rows)
        {
            var color = isAlternateRow ? "grey" : "blue";
            table.AddRow(row.Select(cell => $"[{color}]{cell}[/]").ToArray());
            isAlternateRow = !isAlternateRow;
        }
        Console.Clear();
        AnsiConsole.Write(table);
    }

    internal void ShowEmail(Email email)
    {
        var table = new Table();
        bool isAlternateRow = false;

        table.BorderColor(Color.DarkSlateGray1);
        table.Border(TableBorder.Rounded);
        table.Title("Email Draft");
        table.AddColumn(new TableColumn("[cyan1]From[/]").LeftAligned());
        table.AddColumn(new TableColumn("[green1]To[/]").RightAligned());
        table.AddColumn(new TableColumn("[blue1]Subject[/]").RightAligned());
        table.AddColumn(new TableColumn("[yellow1]Body[/]").RightAligned());
        // table.AddColumn(new TableColumn("[red1]Category[/]").LeftAligned());

        // foreach (string email in emailDetails)
        // {
        var color = isAlternateRow ? "grey" : "blue";
        table.AddRow(
            $"[{color}]{email.FromAddress}[/]",
            $"[{color}]{email.ToAddress}[/]",
            $"[{color}]{email.Subject}[/]",
            $"[{color}]{email.Body}[/]"
        // $"[{color}]{contact.Category.Name}[/]"
        );
        isAlternateRow = !isAlternateRow;
        // }
        Console.Clear();
        AnsiConsole.Write(table);
    }
}
