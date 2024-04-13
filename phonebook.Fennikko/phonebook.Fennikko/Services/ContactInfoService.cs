using System.Net.Mail;
using System.Text.RegularExpressions;
using phonebook.Fennikko.Controllers;
using phonebook.Fennikko.Models;
using PhoneNumbers;
using Spectre.Console;

namespace phonebook.Fennikko.Services;

public class ContactInfoService
{
    private static readonly Regex validEmail = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");

    public static void InsertContact()
    {
        var contact = new ContactInfo
        {
            ContactName = AnsiConsole.Ask<string>("Contact's name: "),
            ContactPhone = GetContactPhoneNumber(),
            ContactEmail = GetContactEmail(),
            CategoryId = AnsiConsole.Confirm("Add category to user?")
            ? CategoryService.GetCategoryOptionInput()?.CategoryId
            : null
        };

        if (contact.CategoryId == null)
        {
            AnsiConsole.Write("No categories available, creating contact with no category. Press any key to continue");
            Console.ReadKey();
        }

        ContactInfoController.AddContact(contact);
    }

    public static void DeleteContact()
    {
        var contact = GetContactInfoOptionInput();
        ContactInfoController.DeleteContact(contact);
    }

    public static void UpdateContact()
    {
        var contact = GetContactInfoOptionInput();
        contact.ContactName = AnsiConsole.Confirm("Update name?")
            ? AnsiConsole.Ask<string>("Contact's new name: ")
            : contact.ContactName;

        contact.ContactPhone = AnsiConsole.Confirm("Update phone number?")
            ? GetContactPhoneNumber()
            : contact.ContactPhone;

        contact.ContactEmail = AnsiConsole.Confirm("Update email?")
            ? GetContactEmail()
            : contact.ContactEmail;

        contact.Category = (AnsiConsole.Confirm("Update category?")
            ? CategoryService.GetCategoryOptionInput()
            : contact.Category)!;

        ContactInfoController.UpdateContact(contact);
    }

    public static void GetContacts()
    {
        var contacts = ContactInfoController.GetContacts();
        UserInterface.ShowContactTable(contacts);
    }

    public static void GetContactById()
    {
        var contact = GetContactInfoOptionInput();
        UserInterface.ShowContact(contact);
    }

    public static ContactInfo GetContactInfoOptionInput()
    {
        var contacts = ContactInfoController.GetContacts();
        var contactsArray = contacts.Select(c => c.ContactName).ToArray();
        if (contactsArray.Length == 0)
        {
            AnsiConsole.Write("No contacts found, press any key to return to the contact menu.");
            Console.ReadKey();
            UserInterface.ContactMenu();
        }

        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose a contact")
            .AddChoices(contactsArray));
        var id = contacts.Single(c => c.ContactName == option).ContactId;
        var contact = ContactInfoController.GetContactById(id);

        return contact;
    }

    public static string GetContactPhoneNumber()
    {
        string? formattedPhoneNumber;
        do
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var rawPhoneNumber = Validator.GetPhoneNumberInput();
            try
            {
                var phoneNumber = phoneNumberUtil.Parse(rawPhoneNumber, null);
                formattedPhoneNumber = phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.E164);
                break;
            }
            catch (NumberParseException e)
            {
                var errorMessage = Convert.ToString(e.Message);
                AnsiConsole.MarkupLine($"[red]{errorMessage}[/] Press any key to continue.");
                Console.ReadKey();
            }
        } while (true);


        return formattedPhoneNumber;
    }

    public static string GetContactEmail()
    {
        do
        {
            var contactEmail = Validator.GetEmailInput();

            if (validEmail.IsMatch(contactEmail))
            {
                return contactEmail;
            }
            else
            {
                AnsiConsole.MarkupLine("[red] Invalid email format.[/] Press any key to continue.");
                Console.ReadKey();
            }

        } while (true);

    }
}