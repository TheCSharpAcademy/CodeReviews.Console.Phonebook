using Phonebook.wkktoria.Models;
using Phonebook.wkktoria.Models.Dtos;
using Phonebook.wkktoria.Services;
using Phonebook.wkktoria.Validators;
using Phonebook.wkktoria.Views;
using Spectre.Console;

namespace Phonebook.wkktoria.Controllers;

public class ContactController
{
    private readonly CategoryService _categoryService = new();
    private readonly ContactService _contactService = new();

    public void AddContact()
    {
        var contact = new Contact
        {
            Name = AnsiConsole.Ask<string>("Name:"),
            Email = GetEmailInput(),
            PhoneNumber = GetPhoneNumberInput(),
            CategoryId = GetCategoryIdOptionInput()
        };

        _contactService.AddContact(contact);
    }

    public void UpdateContact()
    {
        var contact = GetContactOptionInput();

        contact.Name = AnsiConsole.Confirm("Update name?")
            ? AnsiConsole.Ask<string>("Name:")
            : contact.Name;

        contact.Email = AnsiConsole.Confirm("Update email address?")
            ? GetEmailInput()
            : contact.Email;

        contact.PhoneNumber = AnsiConsole.Confirm("Update phone number?")
            ? GetPhoneNumberInput()
            : contact.PhoneNumber;

        _contactService.UpdateContact(contact);
    }

    public void DeleteContact()
    {
        var contact = GetContactOptionInput();

        _contactService.RemoveContact(contact);
    }

    public void ViewContactDetails()
    {
        var contact = GetContactOptionInput();

        ContactView.ShowContactDetails(new ContactDto
        {
            Name = contact.Name,
            Email = contact.Email,
            PhoneNumber = contact.PhoneNumber,
            Category = contact.Category
        });
    }

    public void ViewContacts()
    {
        var contacts = _contactService.GetAllContacts().Select(c => new ContactDto
        {
            Name = c.Name,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
            Category = c.Category
        }).ToList();

        if (contacts.Any())
            ContactView.ShowContactsTable(contacts);
        else
            Console.WriteLine("No contacts found in database.");
    }

    private static string GetEmailInput()
    {
        string email;

        do
        {
            email = AnsiConsole.Ask<string>("Email address (format: name@domain, e.g. john@gmail.com):");

            if (!ContactValidator.IsEmailValid(email)) Outputs.InvalidInputMessage("Invalid email address.");
        } while (!ContactValidator.IsEmailValid(email));

        return email;
    }

    private static string GetPhoneNumberInput()
    {
        string phoneNumber;

        do
        {
            phoneNumber =
                AnsiConsole.Ask<string>("Phone number (format: most of common phone number formats are valid):");

            if (!ContactValidator.IsPhoneNumberValid(phoneNumber))
                Outputs.InvalidInputMessage("Invalid phone number.");
        } while (!ContactValidator.IsPhoneNumberValid(phoneNumber));

        return phoneNumber;
    }

    private Contact GetContactOptionInput()
    {
        var contacts = _contactService.GetAllContacts();
        var contactsDtos = contacts.Select(c => new ContactDto
        {
            Name = c.Name,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
            Category = c.Category
        }.ToString()).ToList();

        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose contact")
            .AddChoices(contactsDtos)
        );

        var selectedContact = option.Split("|");

        var contact = contacts.Single(c =>
            c.Name == selectedContact[0].Trim() && c.Email == selectedContact[1].Trim() &&
            c.PhoneNumber == selectedContact[2].Trim());

        return contact;
    }

    private int GetCategoryIdOptionInput()
    {
        var categories = _categoryService.GetAllCategories();
        var categoriesNames = categories.Select(c => c.Name).ToList();

        var selectedCategory = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose category")
            .AddChoices(categoriesNames));

        var id = categories.Single(c => c.Name == selectedCategory).Id;

        return id;
    }
}