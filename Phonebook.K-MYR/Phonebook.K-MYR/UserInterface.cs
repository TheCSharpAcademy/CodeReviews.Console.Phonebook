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
        while(true)
        {
            Console.Clear();

            var option = AnsiConsole.Prompt(new SelectionPrompt<MainMenu>()
                                                .AddChoices(Enum.GetValues(typeof(MainMenu)).Cast<MainMenu>())
                                                .Title("MainMenu"));

            switch(option)
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
            var option = AnsiConsole.Prompt(new SelectionPrompt<CategoriesMenu>()
                                                .AddChoices(Enum.GetValues(typeof(CategoriesMenu)).Cast<CategoriesMenu>())
                                                .Title("Categories Menu"));

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
                    ViewCategory();
                    break;
                case CategoriesMenu.Exit:
                    exit = true;
                    break;    
            
            }            
        }
    }

    private void ViewCategory()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();

            var category = _categoriesController.GetCategory();

            
        }
    }

    private void ShowContactsMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(new SelectionPrompt<ContactsMenu>()
                                                .AddChoices(Enum.GetValues(typeof(ContactsMenu)).Cast<ContactsMenu>())
                                                .Title("Contacts Menu"));

            switch(option)
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
                        .Title("Contacts");

        foreach (var contact in contacts)
        {
            table.AddRow(contact.Name, contact.EmailAdress, contact.PhoneNumber, contact.Category.Name);
        }

        Console.Clear();
        AnsiConsole.Write(table);
        Console.ReadKey();           
    }

    internal void ShowContact()
    {
        var contact = _contactsController.GetContact();

        var panel = new Panel($"{contact.Name}\n{contact.PhoneNumber}\n{contact.EmailAdress}\n{contact.Category.Name}")
                        .Padding(2,2,2,2)
                        .RoundedBorder();

        Console.Clear();
        AnsiConsole.Write(panel);
        Console.ReadKey();           

    }
}
