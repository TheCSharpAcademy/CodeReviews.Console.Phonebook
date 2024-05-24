using Spectre.Console;
using PhoneBook.DouglasFir.Models;
using PhoneBook.DouglasFir.Services;

namespace PhoneBook.DouglasFir.Utilities;

public static class TableVisualizationEngine
{
    public static void ShowContactsTableWithId(IEnumerable<Contact> contacts)
    {
        string TableTitle = "[yellow]Phonebook[/]";

        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.BorderColor(Color.Grey);
        table.Title(TableTitle);

        string idColumn = $"[bold underline]ID[/]";
        string nameColumn = $"[bold underline]Name[/]";
        string numberColumn = $"[bold underline]Phone Number[/]";
        table.AddColumn(new TableColumn(idColumn).Centered());
        table.AddColumn(new TableColumn(nameColumn).Centered());
        table.AddColumn(new TableColumn(numberColumn).Centered());

        foreach (var contact in contacts)
        {
            table.AddRow(
                contact.Id.ToString()!,
                contact.Name,
                contact.PhoneNumber
            );
        }

        AnsiConsole.Write(table);
        Util.PrintNewLines(2);
    }

    public static void ShowContactsTable(IEnumerable<Contact> contacts)
    {
        string TableTitle = "[yellow]Phonebook[/]";

        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.BorderColor(Color.Grey);
        table.Title(TableTitle);

        string nameColumn = $"[bold underline]Name[/]";
        string numberColumn = $"[bold underline]Phone Number[/]";
        table.AddColumn(new TableColumn(nameColumn).Centered());
        table.AddColumn(new TableColumn(numberColumn).Centered());

        foreach (var contact in contacts)
        {
            table.AddRow(
                contact.Name,
                contact.PhoneNumber
            );
        }

        AnsiConsole.Write(table);
        Util.PrintNewLines(2);
    }
}
