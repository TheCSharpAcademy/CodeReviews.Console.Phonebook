using Bogus;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

internal class ContactMockClass
{
    internal static void Generate()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Generating random contacts.[/]\n");

        var numberOfContacts = GetPositiveNumberInput("Enter a number of contacts to generate:");

        var faker = new Faker();
        var contacts = Enumerable.Range(1, numberOfContacts).Select(_ => new Contact
        {
            Name = faker.Name.FullName(),
            Email = faker.Internet.Email().ToLower(),
            Phone = faker.Phone.PhoneNumber("+# ### ### ####")
        });

        var createContact = new Action(() =>
        {
            using var database = new ContactContext();
            database.Contacts.AddRange(contacts);
            database.SaveChanges();
        });

        if (!ErrorHandler.Success(createContact)) return;
        AnsiConsole.MarkupLine("[green]New contacts added to db successfully![/]");
        DisplayInfoHelpers.PressAnyKeyToContinue();
    }

    private static int GetPositiveNumberInput(string message)
    {
        var input = AnsiConsole.Ask<int>(message);
        while (input <= 0)
        {
            AnsiConsole.Markup("[red]Invalid input. Only positive numbers accepted.[/]\n");
            input = AnsiConsole.Ask<int>("Enter a valid number:");
        }
        return input;
    }

    internal static void DeleteAll()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Deleting all contacts.[/]\n");

        AnsiConsole.MarkupLine($"[red]That action will delete all contacts from the database.[/]");
        if (!DisplayInfoHelpers.ConfirmDeletion())
        {
            Console.Clear();
            return;
        }

        var deleteAllContacts = new Action(() =>
        {
            using var database = new ContactContext();
            database.Contacts.RemoveRange(database.Contacts);
            database.SaveChanges();
            database.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Contacts', RESEED, 0)");
        });

        if (!ErrorHandler.Success(deleteAllContacts)) return;
        AnsiConsole.MarkupLine($"All contact deleted form db successfully.");
        DisplayInfoHelpers.PressAnyKeyToContinue();
    }
}

