using Microsoft.Identity.Client;
using Phonebook.Data;
using Phonebook.Services;

namespace Phonebook.UI;

public class ActionManager
{
    private Menu _menu;
    private ContactService _contactService;
    private ContactCategoryService _contactCategoryService;
    public ActionManager(string dbUser, string dbPassword)
    {
        _menu = new Menu();
        var context = new PhonebookDbContext(dbUser, dbPassword);
        _contactService = new ContactService(context);
        _contactCategoryService = new ContactCategoryService(context);
    }

    public void RunApp()
    {
        bool runApplication = true;
        while(runApplication)
        {
            var choice = _menu.GetMainMenu();
            switch (choice)
            {
                case "Phonebook":
                    ViewPhonebookMenu();
                    break;
                case "Categories":
                    ViewCategoryMenu();
                    break;
                case "Exit":
                    runApplication = false;
                    break;
                default:
                    break;
            }
        }
    }

    private void ViewCategoryMenu()
    {
        while(true)
        {
            var choice = _menu.GetCategoryMenu();
            switch (choice)
            {
                case "View All Categories":
                    _contactCategoryService.ViewAllContactCategories();
                    break;
                case "Add Category":
                    _contactCategoryService.AddContactCategory();
                    break;
                case "Update Category":
                    _contactCategoryService.UpdateContactCategory();
                    break;
                case "[maroon]Go Back[/]":
                    return;
                default:
                    break;
            }
        }
    }

    private void ViewPhonebookMenu()
    {
        while(true)
        {
            var choice = _menu.GetPhonebookMenu();
            switch (choice)
            {
                case "View All Contacts":
                    _contactService.ViewAllContacts();
                    break;
                case "Add Contact":
                    _contactService.AddContact();
                    break;
                case "Update Contact":
                    _contactService.UpdateContact();
                    break;
                case "Delete Contact":
                    _contactService.DeleteContact();
                    break;
                case "[maroon]Go Back[/]":
                    return;
                default:
                    break;
            }
        }
    }

}