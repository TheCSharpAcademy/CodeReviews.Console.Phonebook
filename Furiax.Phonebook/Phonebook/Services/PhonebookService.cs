using Phonebook.Controllers;
using Phonebook.Helpers;
using Phonebook.Model;
using Spectre.Console;

namespace Phonebook.Services;

internal class PhonebookService
{
    internal static void GetContact()
    {
        if (PhonebookController.GetContacts().Count == 0)
        {
            Console.WriteLine("The contactlist is empty");
            Console.ReadKey();
        }
        else
        {
            var contact = GetContactOptionInput();
            UserInterface.DisplayContactTable(contact);
        }
    }

    private static Contact GetContactOptionInput()
    {
        var contacts = PhonebookController.GetContacts();
        var contactsArray = contacts.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose contact")
            .AddChoices(contactsArray));
        var id = contacts.Single(x => x.Name == option).ContactId;
        var contact = PhonebookController.GetContactById(id);
        return contact;
    }

    internal static void GetContacts()
    {
        var contacts = PhonebookController.GetContacts();
        UserInterface.DisplayContactTable(contacts);
    }

    internal static void InsertContact()
    {
        if (CategoryController.GetCategories().Count() == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The category list is empty, you need to add a category first before you can add a contact.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
        else
        {
            var contact = new Contact();
            contact.Name = AnsiConsole.Ask<string>("Contact's name:");
            string phoneNumber;
            do
            {
                phoneNumber = AnsiConsole.Ask<string>("Contact's phonenumber:");
                if (Validation.IsValidPhoneNumber(phoneNumber))
                    contact.PhoneNumber = phoneNumber;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid phonenumber, a valid phonenumber can only contain digits. Try again");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (!Validation.IsValidPhoneNumber(phoneNumber));

            string emailAddress;
            do
            {
                emailAddress = AnsiConsole.Ask<string>("Contact's email address:");
                if (Validation.IsValidEmail(emailAddress))
                    contact.EmailAddress = emailAddress;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid emailadres, try again.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (!Validation.IsValidEmail(emailAddress));

            contact.CategoryId = CategoryService.GetCategoryOptionInput().CategoryId;
            PhonebookController.AddContact(contact);
            Console.Clear();
        }
    }

    internal static void DeleteContact()
    {
        if (PhonebookController.GetContacts().Count == 0)
        {
            Console.WriteLine("The contactlist is empty");
            Console.ReadKey();
        }
        else
        {
            var contact = GetContactOptionInput();
            PhonebookController.DeleteContact(contact);
        }
    }
    internal static void UpdateContact()
    {
        if (PhonebookController.GetContacts().Count == 0)
        {
            Console.WriteLine("The contactlist is empty");
            Console.ReadKey();
        }
        else
        {
            var contact = GetContactOptionInput();
            contact.Name = AnsiConsole.Confirm("Update name?") ?
                AnsiConsole.Ask<string>("Enter the new name:")
                : contact.Name;
            if (AnsiConsole.Confirm("Update phonenumber ?"))
            {
                string newPhoneNumber;
                do
                {
                    newPhoneNumber = AnsiConsole.Ask<string>("Enter the new phonenumber:");
                    if (!Validation.IsValidPhoneNumber(newPhoneNumber))
                        Console.WriteLine("Invalid phone number, try again");
                } while (!Validation.IsValidPhoneNumber(newPhoneNumber));
                contact.PhoneNumber = newPhoneNumber;
            }
            if (AnsiConsole.Confirm("Update emailaddress?"))
            {
                string newEmailAddress;
                do
                {
                    newEmailAddress = AnsiConsole.Ask<string>("Enter the new emailaddress:");
                    if (!Validation.IsValidEmail(newEmailAddress))
                        Console.WriteLine("Invalid emailaddress");
                } while (!Validation.IsValidEmail(newEmailAddress));
                contact.EmailAddress = newEmailAddress;
            }
            PhonebookController.UpdateContact(contact);
            Console.Clear();
        }
    }
    internal static void SendSMS()
    {
        if (PhonebookController.GetContacts().Count == 0)
        {
            Console.WriteLine("The contactlist is empty");
            Console.ReadKey();
        }
        else
        {
            var contact = GetContactOptionInput();
            PhonebookController.SendSMS(contact);
        }
    }
    internal static void SendEmail()
    {
        if (PhonebookController.GetContacts().Count == 0)
        {
            Console.WriteLine("The contactlist is empty");
            Console.ReadKey();
        }
        else
        {
            var contact = GetContactOptionInput();
            PhonebookController.SendEmail(contact);
        }
    }
}
