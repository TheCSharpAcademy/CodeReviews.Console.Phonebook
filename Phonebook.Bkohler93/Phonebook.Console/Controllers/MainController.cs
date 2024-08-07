using Phonebook.Console.Config;
using Phonebook.Console.Data;
using Phonebook.Console.Services;

namespace Phonebook.Console.Controllers;

public class MainController {
    public const string Exit = "Exit";
    public const string CreateContact = "Create new contact";
    public const string DeleteContact = "Delete contact";
    public const string ViewContacts = "View contacts";
    public const string UpdateContact = "Update contact";
    public const string SendEmail = "Send email"; 
    private AppDbContext db;
    private ContactService contactService;
    private AppConfig config;

    public MainController(AppDbContext dbContext, AppConfig appConfig)
    {
        db = dbContext; 
        config = appConfig;
        contactService = new(db, config);
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
            case SendEmail:
                contactService.SendEmail();
                break;
        }
    }
}