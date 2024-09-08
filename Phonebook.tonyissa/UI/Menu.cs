using Phonebook.tonyissa.Services;
using Spectre.Console;

namespace Phonebook.tonyissa.UI;

public static class MenuController
{
    public static async Task InitMenu()
    {
        while (true)
        {
            Console.Clear();
            var prompt = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Please select an option")
                .AddChoices([
                    "View all entries",
                    "Find an entry",
                    "Create an entry",
                    "Remove an entry",
                    "Update an entry",
                    "Quit"
                ])
            );

            switch (prompt)
            {
                case "View all entries":
                    await PhonebookService.GetAllContactsAsync();
                    break;
                case "Find an entry":
                    break;
                case "Create an entry":
                    break;
                case "Remove an entry":
                    break;
                case "Update an entry":
                    break;
                default:
                    return;
            }
        }
    }

    public static void PrintContactList(List<Contact> contacts)
    {
        var table = new Table { Title = new TableTitle("Contacts") };
        table.AddColumns(["ID", "Name", "Email", "Phone Number"]);

        foreach (var contact in contacts)
        {
            table.AddRow([$"{contact.ID}", contact.Name, contact.Email, contact.PhoneNumber]);
        }

        AnsiConsole.Write(table);
        AnsiConsole.Write("\nPress any key to continue...");
        Console.ReadKey();
    }
}