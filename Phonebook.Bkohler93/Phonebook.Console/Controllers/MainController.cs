using Phonebook.Console.Data;
using Phonebook.Console.Services;

namespace Phonebook.Console.Controllers;

public class MainController {
    public const string Exit = "Exit";
    public const string CreateContact = "Create new contact";
    public const string DeleteContact = "Delete contact";
    public const string ViewContacts = "View contacts";
    public const string UpdateContact = "Update contact";
    private AppDbContext db;
    private ContactService contactService;

    public MainController(AppDbContext dbContext)
    {
        db = dbContext; 
        contactService = new(db);
    }

    public void HandleChoice(string choice) {
        switch (choice) {
            case CreateContact:
                contactService.CreateContact();
                break;
            case DeleteContact:
                contactService.DeleteContact();
                break;
            case ViewContacts:
                contactService.ViewContacts();
                break;
            case UpdateContact:
                contactService.UpdateContact();
                break;
        }
    }
}