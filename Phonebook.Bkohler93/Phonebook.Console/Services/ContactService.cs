using Microsoft.IdentityModel.Tokens;
using Phonebook.Console.Data;
using Phonebook.Console.Models;
using Phonebook.Console.UserInterface;

namespace Phonebook.Console.Services;

public class ContactService {
    private AppDbContext db;

    public ContactService(AppDbContext dbContext)
    {
        db = dbContext; 
    }

    public void CreateContact()
    {
        var name = UI.GetName();
        var email = UI.GetEmail();

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

        Contact? contact = null;
        while (contact == null) {
            var contactId = UI.GetValidNumber("Enter the [green]id[/] of the contact you wish to delete:");
            contact = contacts.FirstOrDefault(c => c.Id == contactId);

            if (contact == null) {
                UI.Write("[red]No contact with that id found[/]");
            }
        }
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

        Contact? contact = null;
        while (contact == null) {
            var contactId = UI.GetValidNumber("Enter the [green]id[/] of the contact you wish to update:");
            contact = contacts.FirstOrDefault(c => c.Id == contactId);

            if (contact == null) {
                UI.Write("[red]No contact with that id found[/]");
            }
        }

        var name = UI.GetNameOrDefault(contact.Name ?? "");
        var email = UI.GetEmailOrDefault(contact.Email ?? "");
        var phone = UI.GetPhoneNumberOrDefault(contact.PhoneNumber ?? "");

        contact.Name = name;
        contact.Email = email;
        contact.PhoneNumber = phone;
        db.SaveChanges();

        UI.ConfirmMessage("Saved changes");
    }
}