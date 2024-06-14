using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Phonebook.Data;
using Phonebook.Services;

namespace Phonebook.UI;

public class ActionManager
{
    private Menu _menu;
    private ContactService _contactService;
    private ContactCategoryService _contactCategoryService;
    public ActionManager(IConfigurationRoot config)
    {
        _menu = new Menu();
        PhonebookDbContext context = new PhonebookDbContext(config["DatabaseUserID"], config["DatabasePassword"]);
        _contactService = new ContactService(context, config);
        _contactCategoryService = new ContactCategoryService(context);
    }

    public void RunApp()
    {
        Console.Clear();
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
                case "Send Email":
                    _contactService.SendEmail();
                    break;
                case "Send SMS":
                    _contactService.SendSms();
                    break;
                case "View All Contacts":
                    _contactService.ViewAllContacts();
                    break;
                case "Add Contact":
                    _contactService.AddContact(_contactCategoryService.GetAllContactCategories());
                    break;
                case "Update Contact":
                    _contactService.UpdateContact(_contactCategoryService.GetAllContactCategories());
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