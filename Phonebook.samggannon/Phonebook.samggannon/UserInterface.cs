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
                    MenuOptions.UpdateContact,
                    MenuOptions.DeleteContact,
                    MenuOptions.DeveloperDisclaimer,
                    MenuOptions.Quit));

            switch (option)
            {
                case MenuOptions.AddContact:
                    ContactsService.AddContact();
                    break;
                case MenuOptions.ViewAllContacts:
                    ContactsService.ViewAllContacts();
                    Console.WriteLine("Press [enter] to continue");
                    Console.ReadLine();
                    break;
                case MenuOptions.UpdateContact:
                    ContactsService.UpdateContact();
                    break;
                case MenuOptions.DeleteContact:
                    ContactsService.DeleteContact();
                    break;
                case MenuOptions.DeveloperDisclaimer:
                    ShowDevelopersDisclaimerNote();
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

    private static void ShowDevelopersDisclaimerNote()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[bold]Developer's Disclaimer Note:[/]");
        Console.WriteLine("When adding or updating contacts, a test email will be sent to a directory path:");
        Console.WriteLine("User Profile -> Developement -> Console.Phonebook.TestEmails");
        Console.WriteLine("This is a very basic and simple email client designed to simulate sending emails in a production environment using FluentMail");
        Console.WriteLine("It doesn't have basic security features, and it doesn't require certificates, specific ports, or sending emails through a server.");
        Console.WriteLine("Press [enter] to return to the main menu.");
        Console.ReadLine();
    }

    internal static void ShowContactsTable(List<Contact> contacts)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Email Address");
        table.AddColumn("Phone Number");

        foreach (var contact in contacts)
        {
            table.AddRow(
                contact.ContactId.ToString(),
                contact.Name,
                contact.Email,
                contact.PhoneNumber
                );
        }

        AnsiConsole.Write(table);
    }

    internal static void ConfirmContact(Contact contact)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Email Address");
        table.AddColumn("Phone Number");

        table.AddRow(
            contact.ContactId.ToString(),
            contact.Name,
            contact.Email,
            contact.PhoneNumber
            );

        AnsiConsole.Write(table);
    }
}
