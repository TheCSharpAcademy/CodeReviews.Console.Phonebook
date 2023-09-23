using PhoneBook.Models;
using PhoneBook.Services;
using Spectre.Console;
using static PhoneBook.Enums;

namespace PhoneBook;

internal static class UserInterface
{
    private static void Title()
    {
        AnsiConsole.Write(new FigletText("Phonebook").Color(Color.Green));
    }

    internal static void MainMenu()
    {
        var isAppRunning = true;

        while (isAppRunning)
        {
            AnsiConsole.Clear();
            Title();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                    .Title("Main Menu")
                    .AddChoices(
                        MainMenuOptions.ListContacts,
                        MainMenuOptions.AddContact,
                        MainMenuOptions.Quit));

            switch (option)
            {
                case MainMenuOptions.ListContacts:
                    ContactService.ListContacts();
                    break;
                case MainMenuOptions.AddContact:
                    ContactService.InsertContact();
                    break;
                case MainMenuOptions.Quit:
                default:
                    isAppRunning = false;
                    break;
            }
        }
    }

    internal static void ShowContactDetails(Contact contact)
    {
        AnsiConsole.Clear();
        Title();
        var table = new Table();
        table.AddColumn("Name");
        table.AddColumn("Phone");
        table.AddColumn("Email");

        table.AddRow(contact.Name, contact.Phone, contact.Email);

        AnsiConsole.Write(table);
        ContactOptionMenu(contact);
    }

    private static void ContactOptionMenu(Contact contact)
    {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<ContactOptions>()
                    .Title("Contact Options")
                    .AddChoices(
                        ContactOptions.UpdateContact,
                        ContactOptions.DeleteContact,
                        ContactOptions.GoBack));

            switch (option)
            {
                case ContactOptions.UpdateContact:
                    ContactService.UpdateContact(contact);
                    ShowContactDetails(contact);
                    break;
                case ContactOptions.DeleteContact:
                    if (AnsiConsole.Confirm(
                            "Are you sure you want to delete this contact?"))
                    {
                        ContactService.DeleteContact(contact);
                    }
                    ShowContactDetails(contact);
                    break;
                case ContactOptions.GoBack:
                default:
                    break;
            }
    }

  
}