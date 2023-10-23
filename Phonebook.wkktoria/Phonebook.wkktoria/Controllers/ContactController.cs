using System.Text.RegularExpressions;
using Phonebook.wkktoria.Models;
using Phonebook.wkktoria.Models.Dtos;
using Phonebook.wkktoria.Services;
using Phonebook.wkktoria.Views;
using Spectre.Console;

namespace Phonebook.wkktoria.Controllers;

public class ContactController
{
    private readonly ContactService _contactService = new();

    public void AddContact()
    {
        var contact = new Contact
        {
            Name = AnsiConsole.Ask<string>("Name:"),
            Email = AnsiConsole.Ask<string>("Email Address:"),
            PhoneNumber = AnsiConsole.Ask<string>("Phone Number:")
        };

        _contactService.AddContact(contact);
    }

    public void UpdateContact()
    {
        var contact = GetContactOptionInput();

        contact!.Name = AnsiConsole.Confirm("Update name?")
            ? AnsiConsole.Ask<string>("Name:")
            : contact.Name;

        contact.Email = AnsiConsole.Confirm("Update email address?")
            ? AnsiConsole.Ask<string>("Email Address:")
            : contact.Email;

        contact.PhoneNumber = AnsiConsole.Confirm("Update phone number?")
            ? AnsiConsole.Ask<string>("Phone Number:")
            : contact.PhoneNumber;

        _contactService.UpdateContact(contact);
    }

    public void DeleteContact()
    {
        var contact = GetContactOptionInput();

        _contactService.RemoveContact(contact!);
    }

    public void ViewContactDetails()
    {
        var contact = GetContactOptionInput();

        ContactView.ShowContactDetails(new ContactDto
        {
            Name = contact!.Name,
            Email = contact.Email,
            PhoneNumber = contact.PhoneNumber
        });
    }

    public void ViewContacts()
    {
        var contacts = new ContactService().GetContacts().Select(c => new ContactDto
        {
            Name = c.Name,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber
        }).ToList();

        ContactView.ShowContactsTable(contacts);
    }

    private Contact? GetContactOptionInput()
    {
        var contacts = _contactService.GetContacts();
        var contactsDtos = contacts.Select(c => new ContactDto
        {
            Name = c.Name,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber
        }.ToString()).ToList();

        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose contact")
            .AddChoices(contactsDtos)
        );

        var selectedContact = Regex.Replace(option, @"\s+", string.Empty).Split("|");

        var contact = contacts.Find(c =>
            c.Name == selectedContact[0] && c.Email == selectedContact[1] && c.PhoneNumber == selectedContact[2]);

        return contact;
    }
}