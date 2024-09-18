using PhoneBook.Models;
using PhoneBook.Services;
using Spectre.Console;
using static PhoneBook.Enums;

namespace PhoneBook;

internal class UserInterface
{
    static internal void MainMenu()
    {
        var isAppRunning = true;
        while (isAppRunning)
        {
            Console.Clear();
            
            var menuOptions = Enum.GetValues<MainMenuOptions>()
                .Select(option => option.MainMenuDisplayOptions())
                .ToList();

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose an option:")
                    .AddChoices(menuOptions));

            var selectedOption = Enum.GetValues<MainMenuOptions>()
                .First(option => option.MainMenuDisplayOptions() == selection);

            switch (selectedOption)
            {
                case MainMenuOptions.AddContact:
                    ContactServices.InsertContact();
                    break;
                case MainMenuOptions.DeleteContact:
                    ContactServices.DeleteContact();
                    break;
                case MainMenuOptions.UpdateContact:
                    ContactServices.UpdateContact();
                    break;
                case MainMenuOptions.ViewContacts:
                    ContactServices.GetContacts();
                    break;
                case MainMenuOptions.Quit:
                    Console.WriteLine("Goodbye");
                    isAppRunning = false;
                    break;
            }
        }
    }

    static internal void ShowContactsTable(List<Contact> contacts)
    {
        var table = new Table();

        table.Title = new TableTitle("Your Contacts");

        table.AddColumn("Name");
        table.AddColumn("Phone Number");
        table.AddColumn("Email Address");

        foreach (Contact contact in contacts)
        {
            table.AddRow(
                contact.Name,
                contact.PhoneNumber,
                contact.Email
                );
        }

        AnsiConsole.Write(table);
        
        Console.WriteLine("\nEnter any key to return to the main menu.\n");
        Console.ReadLine();
        Console.Clear();
    }
}
