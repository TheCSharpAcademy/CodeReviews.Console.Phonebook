using Phonebook.wkktoria.Controllers;
using Phonebook.wkktoria.Models;
using Phonebook.wkktoria.Models.Dtos;
using Phonebook.wkktoria.Validators;
using Phonebook.wkktoria.Views;
using Spectre.Console;

namespace Phonebook.wkktoria.Services;

public class ContactService
{
    private readonly CategoryController _categoryController = new();
    private readonly ContactController _contactController = new();

    public void AddContact()
    {
        var contact = new Contact
        {
            Name = AnsiConsole.Ask<string>("Name:"),
            Email = GetEmailInput(),
            PhoneNumber = GetPhoneNumberInput(),
            CategoryId = GetCategoryIdOptionInput()
        };

        _contactController.AddContact(contact);
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

        _contactController.UpdateContact(contact);
    }

    public void DeleteContact()
    {
        var contact = GetContactOptionInput();

        _contactController.RemoveContact(contact);
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
        var contacts = _contactController.GetAllContacts().Select(c => new ContactDto
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
        var contacts = _contactController.GetAllContacts();
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
        var categories = _categoryController.GetAllCategories();
        var categoriesNames = categories.Select(c => c.Name).ToList();

        var selectedCategory = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose category")
            .AddChoices(categoriesNames));

        var id = categories.Single(c => c.Name == selectedCategory).Id;

        return id;
    }
}