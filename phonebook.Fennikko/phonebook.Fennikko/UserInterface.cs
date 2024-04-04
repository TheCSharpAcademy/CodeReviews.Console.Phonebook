using System.Diagnostics.CodeAnalysis;
using phonebook.Fennikko.Models;
using phonebook.Fennikko.Services;
using Spectre.Console;

namespace phonebook.Fennikko;

public class UserInterface
{
    public static void MainMenu()
    {
        var isAppRunning = true;

        do
        {
            AnsiConsole.Clear();
            var options = AnsiConsole.Prompt(
                new SelectionPrompt<Enums.MainMenuOptions>()
                    .Title("What would you like to do?")
                    .AddChoices(
                        Enums.MainMenuOptions.ManageCategories,
                        Enums.MainMenuOptions.ManageContacts,
                        Enums.MainMenuOptions.Quit));
            switch (options)
            {
                case Enums.MainMenuOptions.ManageCategories:
                    CategoryMenu();
                    break;
                case Enums.MainMenuOptions.ManageContacts:
                    ContactMenu();
                    break;
                case Enums.MainMenuOptions.Quit:
                    isAppRunning = false;
                    Environment.Exit(0);
                    break;
            }
        } while (isAppRunning);
    }

    private static void ContactMenu()
    {
        var contactMenuRunning = true;
        do
        {
            AnsiConsole.Clear();
            var options = AnsiConsole.Prompt(
                new SelectionPrompt<Enums.ContactMenu>()
                    .Title("What would you like to do?")
                    .AddChoices(
                        Enums.ContactMenu.AddContact,
                        Enums.ContactMenu.DeleteContact,
                        Enums.ContactMenu.UpdateContact,
                        Enums.ContactMenu.ViewContact,
                        Enums.ContactMenu.ViewAllContacts,
                        Enums.ContactMenu.GoBack));

            switch (options)
            {
                case Enums.ContactMenu.AddContact:
                    ContactInfoService.InsertContact();
                    break;
                case Enums.ContactMenu.DeleteContact:
                    ContactInfoService.DeleteContact();
                    break;
                case Enums.ContactMenu.UpdateContact:
                    ContactInfoService.UpdateContact(); 
                    break;
                case Enums.ContactMenu.ViewContact:
                    ContactInfoService.GetContactById();
                    break;
                case Enums.ContactMenu.ViewAllContacts:
                    ContactInfoService.GetContacts();
                    break;
                case Enums.ContactMenu.GoBack:
                    contactMenuRunning = false;
                    MainMenu();
                    break;
            }

        } while (contactMenuRunning);
    }

    private static void CategoryMenu()
    {
        throw new NotImplementedException();
    }

    public static void ShowContactTable(List<ContactInfo> contacts)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Phone number");
        table.AddColumn("Email address");
        table.AddColumn("Category");

        foreach (var contact in contacts)
        {
            table.AddRow(
                contact.ContactId.ToString(),
                contact.ContactName,
                contact.ContactPhone,
                contact.ContactEmail,
                contact.Category.Name
            );
        }

        AnsiConsole.Write(table);

        AnsiConsole.Write("Press any key to continue");
        Console.ReadKey();
        AnsiConsole.Clear();
    }

    public static void ShowContact(ContactInfo contact)
    {
        var panel = new Panel($"""
                               Id: {contact.ContactId}
                               Category: {contact.Category.Name}
                               """)
        {
            Header = new PanelHeader($"{contact.ContactName}"),
            Padding = new Padding(2, 2, 2, 2)
        };

        AnsiConsole.Write(panel);

        AnsiConsole.Write("Press any key to continue");
        Console.ReadKey();
        AnsiConsole.Clear();
    }
}