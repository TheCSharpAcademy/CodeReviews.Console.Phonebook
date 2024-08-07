using Microsoft.IdentityModel.Tokens;
using Phonebook.Console.Config;
using Phonebook.Console.Data;
using Phonebook.Console.Models;
using Phonebook.Console.UserInterface;

namespace Phonebook.Console.Services;

public class ContactService {
    private AppDbContext db;
    private AppConfig config;
    private EmailService emailService;

    public ContactService(AppDbContext dbContext, AppConfig appConfig)
    {
        db = dbContext; 
        config = appConfig;
        emailService = new(config);
    }

    public void CreateContact()
    {
        var name = UI.GetName();
        var email = UI.GetEmail("Enter the contact's [green]email[/][grey] formatted as 'name@gmail.com'[/]:");

        var phone = UI.GetPhoneNumber();

        var contact = db.Add(new Contact{
            Name = name,
            Email = email,
            PhoneNumber = phone,
        });
        UI.ConfirmMessage($"Created contact for '{contact.Entity.Name}'.");
        db.SaveChanges();
    }

    public void ViewContacts()
    {
        var contacts = db.Contacts.ToList();

        if (contacts.IsNullOrEmpty()) {
            UI.ConfirmMessage("You have no contacts to view yet");
            return;
        }

        UI.ViewContacts(contacts);
        UI.ConfirmMessage("");
    }

    public void DeleteContact()
    {
       var contacts = db.Contacts.ToList();

        if (contacts.IsNullOrEmpty()) {
            UI.ConfirmMessage("You have no contacts to delete yet");
            return;
        }

        UI.ViewContacts(contacts);

        var contact = RetrieveContactFromList(contacts);

        db.Contacts.Attach(contact);
        db.Contacts.Remove(contact);
        db.SaveChanges();

        UI.ConfirmMessage($"Contact '{contact.Name}' deleted.");
    }

    public void UpdateContact()
    {
        var contacts = db.Contacts.ToList();

        if (contacts.IsNullOrEmpty()) {
            UI.ConfirmMessage("You have no contacts to update yet");
            return;
        }

        UI.ViewContacts(contacts);

        var contact = RetrieveContactFromList(contacts);

        var name = UI.GetNameOrDefault(contact.Name ?? "");
        var email = UI.GetEmailOrDefault(contact.Email ?? "");
        var phone = UI.GetPhoneNumberOrDefault(contact.PhoneNumber ?? "");

        contact.Name = name;
        contact.Email = email;
        contact.PhoneNumber = phone;
        db.SaveChanges();

        UI.ConfirmMessage("Saved changes");
    }

    public void SendEmail()
    {
        var contacts = db.Contacts.ToList();

        if (contacts.IsNullOrEmpty())
        {
            UI.ConfirmMessage("You have no contacts to update yet");
            return;
        }

        UI.ViewContacts(contacts);
        var contact = RetrieveContactFromList(contacts);

        try
        {
            emailService.SendEmail(contact);

            UI.ConfirmMessage("Email sent successfully!");
        }
        catch (Exception e)
        {
            UI.ConfirmMessage($"Email was not sent: {e.Message}");
        }
    }

    private static Contact RetrieveContactFromList(List<Contact> contacts)
    {
        Contact? contact = null;
        while (contact == null)
        {
            var contactId = UI.GetValidNumber("Enter the [green]id[/] of the contact you wish to send an email to:");
            contact = contacts.FirstOrDefault(c => c.Id == contactId);

            if (contact == null)
            {
                UI.Write("[red]No contact with that id found[/]");
            }
        }

        return contact;
    }
}