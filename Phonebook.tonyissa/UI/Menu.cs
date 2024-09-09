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
                    "View all contacts",
                    "Find/update/delete a contact",
                    "Create a new contact",
                    "Quit"
                ])
            );

            switch (prompt)
            {
                case "View all contacts":
                    await PhonebookService.GetAllContactsAsync();
                    break;
                case "Find/update/delete a contact":
                    await PhonebookService.GetSingularContact();
                    break;
                case "Create a new contact":
                    await PhonebookService.CreateContact();
                    break;
                default:
                    return;
            }
        }
    }

    public static void PrintContacts(List<Contact> contacts)
    {
        Console.Clear();
        if (contacts.Count < 1)
        {
            Console.WriteLine("No contacts found");
            return;
        }

        var table = new Table { Title = new TableTitle("Contacts") };
        table.AddColumns(["ID", "Name", "Email", "Phone Number"]);

        foreach (var contact in contacts)
        {
            var contactID = contacts.IndexOf(contact) + 1;
            table.AddRow([contactID.ToString(), contact.Name, contact.Email, contact.PhoneNumber]);
        }

        AnsiConsole.Write(table);
    }
}