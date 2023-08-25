using PhoneBookCarDioLogics.Controllers;
using PhoneBookCarDioLogics.Models;
using Spectre.Console;
using static PhoneBookCarDioLogics.Enums;

namespace PhoneBookCarDioLogics;

internal class UserInterface
{
    internal static void MainMenu()
    {
        bool appIsRunning = true;

        while(appIsRunning == true)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
        new SelectionPrompt<MainMenuOptions>()
        .Title("What would you like to do?")
        .AddChoices(
            MainMenuOptions.ManageCategories,
            MainMenuOptions.ManageContacts,
            MainMenuOptions.SendEmail,
            MainMenuOptions.Quit));

            switch (option)
            {
                case MainMenuOptions.ManageCategories:
                    CategoriesMenu();
                    break;
                case MainMenuOptions.ManageContacts:
                    ContactsMenu();
                    break;
                case MainMenuOptions.SendEmail:
                    ContactsController.PrepareAndSendEmail();
                    break;
                case MainMenuOptions.Quit:
                    appIsRunning = false;
                    break;
            }
        }
    }

    internal static void CategoriesMenu()
    {
        bool categoriesMenuIsRunning = true;

        while (categoriesMenuIsRunning == true)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
        new SelectionPrompt<CategoriesMenuOptions>()
        .Title("What would you like to do?")
        .AddChoices(
            CategoriesMenuOptions.AddCategory,
            CategoriesMenuOptions.ShowCategoryTable,
            CategoriesMenuOptions.DeleteCategory,
            CategoriesMenuOptions.UpdateCategory,
            CategoriesMenuOptions.BackToMainMenu));

            switch (option)
            {
                case CategoriesMenuOptions.AddCategory:
                    CategoryController.InsertCategory();
                    break;
                case CategoriesMenuOptions.ShowCategoryTable:
                    CategoryController.GetCategories();
                    break;
                case CategoriesMenuOptions.DeleteCategory:
                    CategoryController.DeleteCategory();
                    break;
                case CategoriesMenuOptions.UpdateCategory:
                    CategoryController.UpdateCategory();
                    break;
                case CategoriesMenuOptions.BackToMainMenu:
                    categoriesMenuIsRunning = false;
                    break;
            }
        }
    }

    internal static void ContactsMenu()
    {
        bool contactsMenuIsRunning = true;

        while (contactsMenuIsRunning == true)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
        new SelectionPrompt<ContactsMenuOptions>()
        .Title("What would you like to do?")
        .AddChoices(
            ContactsMenuOptions.AddContact,
            ContactsMenuOptions.DeleteContact,
            ContactsMenuOptions.UpdateContact,
            ContactsMenuOptions.ViewContacts,
            ContactsMenuOptions.BackToMainMenu));

            switch (option)
            {
                case ContactsMenuOptions.AddContact:
                    ContactsController.InsertContact();
                    break;
                case ContactsMenuOptions.DeleteContact:
                    ContactsController.RemoveContact();
                    break;
                case ContactsMenuOptions.UpdateContact:
                    ContactsController.ChangeContact();
                    break;
                case ContactsMenuOptions.ViewContacts:
                    ContactsController.ShowContacts();
                    break;
                case ContactsMenuOptions.BackToMainMenu:
                    contactsMenuIsRunning = false;
                    break;
            }
        }
    }

    internal static void ShowContacts(List<Contact> contacts)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("PhoneNumber");
        table.AddColumn("Email");
        table.AddColumn("Category");

        foreach (Contact contact in contacts)
        {
            table.AddRow(
                contact.ContactId.ToString(),
                contact.Name,
                contact.PhoneNumber,
                contact.Email,
                contact.Category.CategoryName
                );
        }

        AnsiConsole.Write(table);
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }

    internal static void ShowCategories(List<Category> categories)
    {
        var table = new Table();
        table.AddColumn("CategoryId");
        table.AddColumn("CategoryName");

        foreach (Category category in categories)
        {
            table.AddRow(
                category.CategoryId.ToString(),
                category.CategoryName
                );
        }

        AnsiConsole.Write(table);
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }
}
