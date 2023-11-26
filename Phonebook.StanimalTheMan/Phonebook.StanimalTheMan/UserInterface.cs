using Phonebook.StanimalTheMan.Models;
using Phonebook.StanimalTheMan.Services;
using Spectre.Console;
using static Phonebook.StanimalTheMan.Enums;

namespace Phonebook.StanimalTheMan;

static internal class UserInterface
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
                    MenuOptions.DeleteContact,
                    MenuOptions.UpdateContact,
                    MenuOptions.ViewAllContacts,
                    MenuOptions.ViewContact,
                    MenuOptions.Quit));

            switch (option)
            {
                case MenuOptions.AddContact:
                    ContactService.InsertContact();
                    break;
                case MenuOptions.DeleteContact:
                    ContactService.DeleteContact();
                    break;
                case MenuOptions.UpdateContact:
                    ContactService.UpdateContact();
                    break;
                case MenuOptions.ViewAllContacts:
                    ContactService.GetContacts();
                    break;
                case MenuOptions.ViewContact:
                    ContactService.GetContact();
                    break;
                case MenuOptions.Quit:
                    Console.WriteLine("Goodbye");
                    isAppRunning = false;
                    break;
            }
        }
    }

    internal static void ShowContact(Contact contact)
    {
        var panel = new Panel($@"Id: {contact.Id}
Name: {contact.Name}
Email: {contact.Email}
Phone Number: {contact.PhoneNumber}");
        panel.Header = new PanelHeader("Contact Info");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);

        Console.WriteLine("Press Any Key to Return to Menu");
        Console.ReadLine();
        Console.Clear();
    }

    internal static void ShowContactTable(List<Contact> contacts)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Email");
        table.AddColumn("Phone Number");

        foreach (Contact contact in contacts)
        {
            table.AddRow(
                contact.Id.ToString(),
                contact.Name,
                contact.Email,
                contact.PhoneNumber
                );
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Enter any key to continue");
        Console.ReadLine();
        Console.Clear();
    }
}
