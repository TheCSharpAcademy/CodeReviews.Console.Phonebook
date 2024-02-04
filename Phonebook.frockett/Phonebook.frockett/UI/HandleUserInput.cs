
using Phonebook.frockett.DTOs;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.frockett.UI;

public class HandleUserInput
{
    private readonly InputValidator validator;

    public HandleUserInput(InputValidator validator) 
    {
        this.validator = validator;
    }

    public ContactDTO GetEditedContact(ContactDTO oldContact)
    {
        ContactDTO updatedContact = new();

        if (AnsiConsole.Confirm("Replace name? "))
        {
            updatedContact.Name = GetName();
        }
        else
        {
            updatedContact.Name = oldContact.Name;
        }

        if (AnsiConsole.Confirm("Edit email? "))
        {
            updatedContact.Email = GetEmail();
        }
        else
        {
            updatedContact.Email = oldContact.Email;
        }

        if (AnsiConsole.Confirm("Edit phone number? "))
        {
            updatedContact.PhoneNumber = GetPhoneNumber();
        }
        else
        {
            updatedContact.PhoneNumber = oldContact.PhoneNumber;
        }

        if (oldContact.ContactGroupName != null)
        {
            updatedContact.ContactGroupName = oldContact.ContactGroupName;
        }

        return updatedContact;
    }
    public string GetPhoneNumber()
    {
        string newPhoneNumber = AnsiConsole.Ask<string>("Enter the contact's phone number (Format 0123456789 or 012-345-6789): ");
        while (!validator.IsValidPhoneNumber(newPhoneNumber))
        {
            newPhoneNumber = AnsiConsole.Ask<string>($"{newPhoneNumber} is not a valid number. Use format 0123456789 or 012-345-6789. Enter number: ");
        }

        return newPhoneNumber;
    }

    public string GetEmail()
    {
        string newEmail = AnsiConsole.Ask<string>("Enter the contact's email: ");
        while (!validator.IsValidEmail(newEmail))
        {
            newEmail = AnsiConsole.Ask<string>($"{newEmail} is not a valid email. Use the full address, ex. user@domain.com. Enter email: ");
        }

        return newEmail;
    }

    public string GetName()
    {
        string newName = AnsiConsole.Ask<string>("Enter the name: ");
        while (!validator.IsValidName(newName))
        {
            newName = AnsiConsole.Ask<string>($"{newName} is not a valid name. Name can't be null or empty. Enter a valid name: ");
        }

        return newName;
    }

    public ContactDTO SelectContact(List<ContactDTO> contacts)
    {
        AnsiConsole.Clear();

        var selectOptions = new SelectionPrompt<ContactDTO>();
        selectOptions.AddChoice(new ContactDTO { Email = "0" }); // Use what would otherwise be an invalid email to identify the "cancel" button
        selectOptions.AddChoices(contacts);
        selectOptions.UseConverter(contact => (contact.Email == "0" ? "Cancel" : $"{contact.Name} - {contact.PhoneNumber} - {contact.Email}") // if email is 0 it's a cancel button
                                                + (contact.ContactGroupName != null ? $" - {contact.ContactGroupName}" : "")); // if contact has group name, add it too
        selectOptions.Title("Select the group using the arrow and enter keys");
        selectOptions.MoreChoicesText("Keep scrolling for more");

        ContactDTO selectedContact = AnsiConsole.Prompt(selectOptions);

        return selectedContact;
    }

    public ContactGroupDTO SelectGroup(List<ContactGroupDTO> contactGroups)
    {
        AnsiConsole.Clear();
        var selectOptions = new SelectionPrompt<ContactGroupDTO>();
        selectOptions.AddChoice(new ContactGroupDTO { Name = " " }); // invalid group name is used to identfy the cancel button
        selectOptions.AddChoices(contactGroups);
        selectOptions.UseConverter(group => (group.Name == "0" ? "Cancel" : $"{group.Name}")); // if email is 0 it's a cancel button 
        selectOptions.Title("Select the group using the arrow and enter keys for more options");
        selectOptions.MoreChoicesText("Keep scrolling for more");

        ContactGroupDTO selectedGroup = AnsiConsole.Prompt(selectOptions);

        return selectedGroup;
    }
}
