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
        var categoryMenuRunning = true;

        do
        {
            AnsiConsole.Clear();
            var options = AnsiConsole.Prompt(
                new SelectionPrompt<Enums.CategoryMenu>()
                    .Title("What would you like to do?")
                    .AddChoices(
                        Enums.CategoryMenu.AddCategory,
                        Enums.CategoryMenu.DeleteCategory,
                        Enums.CategoryMenu.UpdateCategory,
                        Enums.CategoryMenu.ViewCategory,
                        Enums.CategoryMenu.ViewAllCategories,
                        Enums.CategoryMenu.GoBack));

            switch (options)
            {
                case Enums.CategoryMenu.AddCategory:
                    CategoryService.InsertCategory();
                    break;
                case Enums.CategoryMenu.DeleteCategory:
                    CategoryService.DeleteCategory();
                    break;
                case Enums.CategoryMenu.UpdateCategory:
                    CategoryService.UpdateCategory();
                    break;
                case Enums.CategoryMenu.ViewCategory:
                    CategoryService.GetCategory();
                    break;
                case Enums.CategoryMenu.ViewAllCategories:
                    CategoryService.GetCategories();
                    break;
                case Enums.CategoryMenu.GoBack:
                    categoryMenuRunning = false;
                    MainMenu();
                    break;
            }
        } while (categoryMenuRunning);
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
            var category = contact.Category != null ? contact.Category.Name : "None";

            table.AddRow(
                contact.ContactId.ToString(),
                contact.ContactName,
                contact.ContactPhone,
                contact.ContactEmail,
                category
            );
        }

        AnsiConsole.Write(table);

        AnsiConsole.Write("Press any key to continue");
        Console.ReadKey();
        AnsiConsole.Clear();
    }

    public static void ShowContact(ContactInfo contact)
    {
        var category = contact.Category != null ? contact.Category.Name : "None";

        var panel = new Panel($"""
                               Id: {contact.ContactId}
                               Category: {category}
                               Phone Number: {contact.ContactPhone}
                               Email: {contact.ContactEmail}
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

    public static void ShowCategoryTable(List<Category> categories)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");

        foreach (var category in categories)
        {
            table.AddRow(
                category.CategoryId.ToString(),
                category.Name
            );
        }

        AnsiConsole.Write(table);

        AnsiConsole.Write("Press any key to continue");
        Console.ReadKey();
        AnsiConsole.Clear();
    }

    public static void ShowCategory(Category category)
    {
        var panel = new Panel($"""
                               Id: {category.CategoryId}
                               Contacts: {category.Contacts.Count}
                               """)
        {
            Header = new PanelHeader($"{category.Name}"),
            Padding = new Padding(2, 2, 2, 2)
        };

        AnsiConsole.Write(panel);

        ShowContactTable(category.Contacts);
    }
}