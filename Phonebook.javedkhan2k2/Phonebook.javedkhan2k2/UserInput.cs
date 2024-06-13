
using Phonebook.Entities;
using Spectre.Console;
using Phonebook.Validators;
using Phonebook.UI;
using Phonebook.Constants;
using Phonebook.Repositories;

namespace Phonebook;

public class UserInput
{
    internal static Contact? GetNewContact(IEnumerable<ContactCategory> contactCategories)
    {
        AnsiConsole.Clear();
        Contact contact = new Contact();
        contact.Name = UserInput.GetStringInput(Messages.NameMessage);
        if (string.IsNullOrEmpty(contact.Name)) return null;

        contact.Email = UserInput.GetEmailInput(Messages.EmailMessage);
        if (string.IsNullOrEmpty(contact.Email)) return null;

        contact.PhoneNumber = UserInput.GetPhoneInput(Messages.PhoneNumberMessage);
        if (string.IsNullOrEmpty(contact.PhoneNumber)) return null;

        var choice = Menu.GetContactCategoryMenu(contactCategories);
        if (choice == Menu.CancelOperation) return null;
        var temp = contactCategories.FirstOrDefault(c => c.CategoryName == choice);
        if (temp == null) return null;
        contact.ContactCategoryId = temp.Id;

        return contact;
    }

    internal static bool UpdateContact(Contact contact, IEnumerable<ContactCategory> contactCategories)
    {
        contact.Name = AnsiConsole.Confirm($"Do you want to Update Name([maroon]{contact.Name}[/])") ? GetStringInput(Messages.NameMessage) : contact.Name;
        if (string.IsNullOrEmpty(contact.Name)) return false;

        contact.Email = AnsiConsole.Confirm($"Do you want to Update Email([maroon]{contact.Email}[/])") ? GetEmailInput(Messages.EmailMessage) : contact.Email;
        if (string.IsNullOrEmpty(contact.Email)) return false;

        contact.PhoneNumber = AnsiConsole.Confirm($"Do you want to Update Phone Number([maroon]{contact.PhoneNumber}[/])") ? GetPhoneInput(Messages.PhoneNumberMessage) : contact.PhoneNumber;
        if (string.IsNullOrEmpty(contact.PhoneNumber)) return false;

        if (AnsiConsole.Confirm($"Do you want to Update Contact Category([maroon]{contact.ContactCategory.CategoryName}[/])?"))
        {
            var choice = Menu.GetContactCategoryMenu(contactCategories);
            if (choice == Menu.CancelOperation) return false;
            var temp = contactCategories.FirstOrDefault(c => c.CategoryName == choice);
            if (temp == null) return false;
            contact.ContactCategoryId = temp.Id;
        }
        return true;

    }

    public static string GetStringInput(string message)
    {
        string input = AnsiConsole.Ask<string>(message).Trim();
        if (input == "0") return "";
        while (!ValidatorHelper.IsValidName(input))
        {
            input = AnsiConsole.Ask<string>($"Invalid name [maroon]{input}[/] entered. {message}").Trim();
            if (input == "0") return "";
        }
        return input;
    }

    internal static string GetEmailInput(string message)
    {
        string input = AnsiConsole.Ask<string>(message).Trim();
        if (input == "0") return "";
        while (!ValidatorHelper.IsValidEmail(input))
        {
            input = AnsiConsole.Ask<string>($"Invalid email [maroon]{input}[/] entered. {message}").Trim();
            if (input == "0") return "";
        }
        return input;
    }

    internal static string GetPhoneInput(string message)
    {
        string input = AnsiConsole.Ask<string>(message).Trim();
        if (input == "0") return "";
        while (!ValidatorHelper.IsValidPhoneNumber(input))
        {
            input = AnsiConsole.Ask<string>($"Invalid phone number [maroon]{input}[/] entered. {message}").Trim();
            if (input == "0") return "";
        }
        return input;
    }

    internal static int GetIntInput()
    {
        int id = AnsiConsole.Ask<int>("Enter contact Id from the table");
        return id;
    }

    internal static ContactCategory GetNewContactCategory(ContactCategoryRepository repository)
    {
        var contactCategory = new ContactCategory();
        contactCategory.CategoryName = UserInput.GetStringInput("Enter A Category Name Or Enter [green]0[/] to Cancel: ");
        if (contactCategory.CategoryName == "") return null;

        while (repository.FindContactCategoryByName(contactCategory.CategoryName) != null)
        {
            AnsiConsole.Markup($"The [maroon]{contactCategory.CategoryName}[/] Exists in Database.\n");
            contactCategory.CategoryName = UserInput.GetStringInput("Enter A Category Name: ");
            contactCategory.CategoryName = UserInput.GetStringInput("Enter A Category Name Or Enter [green]0[/] to Cancel: ");
            if (contactCategory.CategoryName == "") return null;
        }
        return contactCategory;
    }
}