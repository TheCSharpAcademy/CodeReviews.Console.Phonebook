using Spectre.Console;
using Phonebook.samggannon.Services;
using static Phonebook.samggannon.Enums;
using Phonebook.samggannon.Models;

namespace Phonebook.samggannon;

internal static class UserInterface
{
    static internal void MainMenu()
    {
        var isAppRunning = true;
        while (isAppRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                .Title("What would you like to do?")
                .AddChoices(
                    MenuOptions.AddContact,
                    MenuOptions.ViewAllContacts,
                    MenuOptions.Quit));

            switch (option)
            {
                case MenuOptions.AddContact:
                    ContactsService.AddContact();
                    break;
                case MenuOptions.ViewAllContacts:
                    ContactsService.ViewAllContacts();
                    break;
                case MenuOptions.ViewContact:
                    ContactsService.ViewContact();
                    break;
                case MenuOptions.UpdateContact:
                    ContactsService.UpdateContact();
                    break;
                case MenuOptions.DeleteContact:
                    ContactsService.DeleteContact();
                    break;
                case MenuOptions.Quit:
                    isAppRunning = false;
                    Environment.Exit(0);
                    break;
                default:
                    isAppRunning = false;
                    break;
            }

        }
    }

    internal static void ShowContactsTable(List<ContactDto> contacts)
    {
        var table = new Table();
        table.AddColumn("Name");
        table.AddColumn("Email Address");
        table.AddColumn("Phone Number");

        foreach (var contact in contacts)
        {
            table.AddRow(
                contact.Name,
                contact.Email,
                contact.PhoneNumber
                );
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
        Console.Clear();
    }
}
