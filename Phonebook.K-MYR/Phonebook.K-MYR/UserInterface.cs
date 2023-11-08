using Spectre.Console;
using static Phonebook.K_MYR.Enums;

namespace Phonebook.K_MYR;

internal class UserInterface
{
    private readonly ContactsController _contactsController;

    private readonly CategoriesController _categoriesController;

    public UserInterface(ContactsController contactsController, CategoriesController categoriesController)
    {
        _contactsController = contactsController;
        _categoriesController = categoriesController;
    }

    internal void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.Write(new Panel("[darkorange3_1] Main Menu [/]").RoundedBorder().Padding(3, 0, 3, 0));
            var option = AnsiConsole.Prompt(new SelectionPrompt<MainMenu>()
                                                .AddChoices(Enum.GetValues(typeof(MainMenu)).Cast<MainMenu>()));

            switch (option)
            {
                case MainMenu.ManageCategories:
                    ShowCategoriesMenu();
                    break;
                case MainMenu.ManageContacts:
                    ShowContactsMenu();
                    break;
                case MainMenu.Exit:
                    Environment.Exit(1);
                    break;
            }
        }
    }

    private void ShowCategoriesMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            AnsiConsole.Write(new Panel("[darkorange3_1] Category Menu [/]")
                                .Padding(3, 0, 3, 0)
                                .BorderColor(Color.DarkOrange3_1));

            var option = AnsiConsole.Prompt(new SelectionPrompt<CategoriesMenu>()
                                                .AddChoices(Enum.GetValues(typeof(CategoriesMenu)).Cast<CategoriesMenu>()));

            switch (option)
            {
                case CategoriesMenu.AddCategory:
                    _categoriesController.AddCategory();
                    break;
                case CategoriesMenu.UpdateCategory:
                    _categoriesController.UpdateCategory();
                    break;
                case CategoriesMenu.DeleteCategory:
                    _categoriesController.DeleteCategory();
                    break;
                case CategoriesMenu.ViewCategory:
                    ShowCategory();
                    break;
                case CategoriesMenu.Exit:
                    exit = true;
                    break;

            }
        }
    }

    private void ShowCategory()
    {
        Console.Clear();

        var category = _categoriesController.GetCategory();

        if (category is null)
            return;

        var table = new Table()
                        .AddColumns("Name", "Email Adress", "Phone Number")
                        .Title($"[darkorange3_1]{category.Name}[/]")
                        .BorderColor(Color.DarkOrange3_1);

        foreach (var contact in category.Contacts)
        {
            table.AddRow(contact.FullName, contact.EmailAdress, contact.PhoneNumber);
        }

        AnsiConsole.Write(table);
        Helpers.WriteMessageAndWait("Press Any Key To Return");
    }

    private void ShowContactsMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            AnsiConsole.Write(new Panel("[darkorange3_1] Contact Menu [/]")
                                .Padding(3, 0, 3, 0)
                                .BorderColor(Color.DarkOrange3_1));

            var option = AnsiConsole.Prompt(new SelectionPrompt<ContactsMenu>()
                                                .AddChoices(Enum.GetValues(typeof(ContactsMenu)).Cast<ContactsMenu>()));

            switch (option)
            {
                case ContactsMenu.AddContact:
                    _contactsController.AddContact();
                    break;
                case ContactsMenu.UpdateContact:
                    _contactsController.UpdateContact();
                    break;
                case ContactsMenu.DeleteContact:
                    _contactsController.DeleteContact();
                    break;
                case ContactsMenu.ViewAllContacts:
                    ShowContacts();
                    break;
                case ContactsMenu.ViewContact:
                    ShowContact();
                    break;
                case ContactsMenu.Exit:
                    exit = true;
                    break;
            }
        }
    }

    private void ShowContacts()
    {
        var contacts = _contactsController.GetAllContacts();

        var table = new Table()
                        .AddColumns("Name", "Email Adress", "Phone Number", "Category")
                        .Title("[darkorange3_1]Contacts[/]")
                        .BorderColor(Color.DarkOrange3_1);

        foreach (var contact in contacts)
        {
            table.AddRow(contact.FullName, contact.EmailAdress, contact.PhoneNumber, contact.CategoryName);
        }

        Console.Clear();
        AnsiConsole.Write(table);
        Helpers.WriteMessageAndWait("Press Any Key To Return");
    }

    internal void ShowContact()
    {
        var contact = _contactsController.GetContact();

        Console.Clear();

        if (contact is null)
            AnsiConsole.Write(new Panel("No Contacts Were Found. Please Add A Contact First").BorderColor(Color.DarkOrange3_1));

        else
            AnsiConsole.Write(new Panel($"{contact.PhoneNumber}\n{contact.EmailAdress}\n{contact.CategoryName}")
                        .Padding(2, 1, 2, 1)
                        .BorderColor(Color.DarkOrange3_1));

        Helpers.WriteMessageAndWait("Press Any Key To Return");
    }
}
