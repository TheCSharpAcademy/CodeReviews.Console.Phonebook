using Microsoft.Identity.Client;
using Phonebook.Data;
using Phonebook.Services;

namespace Phonebook.UI;

public class ActionManager
{
    Menu menu;
    ContactService contactService;

    public ActionManager()
    {
        menu = new Menu();
        contactService = new ContactService(new PhonebookDbContext());
    }

    public void RunApp()
    {
        bool runApplication = true;
        while(runApplication)
        {
            var choice = menu.GetMainMenu();
            switch (choice)
            {
                case "View All Contacts":
                    contactService.ViewAllContacts();
                    break;
                case "Add Contact":
                    contactService.AddContact();
                    break;
                case "Update Contact":
                    contactService.UpdateContact();
                    break;
                case "Delete Contact":
                    contactService.DeleteContact();
                    break;
                case "Exit":
                    runApplication = false;
                    break;
                default:
                    break;
            }
        }
    }
}