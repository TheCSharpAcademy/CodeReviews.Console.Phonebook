using Phonebook.frockett.DTOs;
using Spectre.Console;

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

        updatedContact.Id = oldContact.Id;
        return updatedContact;
    }
    public string GetPhoneNumber()
    {
        string newPhoneNumber = AnsiConsole.Ask<string>("Enter the contact's phone number (Format 0123456789 or 012-345-6789): ");
        while (!validator.IsValidPhoneNumber(newPhoneNumber))
        {
            newPhoneNumber = AnsiConsole.Ask<string>($"{newPhoneNumber} is not a valid number. Use format 0123456789 or 012-345-6789. Enter number: ");
        }

        string formattedNumber = ReformatPhoneNumber(newPhoneNumber);

        return formattedNumber;
    }

    private string ReformatPhoneNumber(string phoneNumber)
    {
        // Remove all non-numeric characters
        string digits = new String(phoneNumber.Where(char.IsDigit).ToArray());

        // Check if we have exactly 10 digits, which is valid for US phone numbers
        if (digits.Length == 10)
        {
            // Reformat to (555) 555-5555
            return $"({digits.Substring(0, 3)}) {digits.Substring(3, 3)}-{digits.Substring(6)}";
        }

        // Return the original string if it doesn't match expected format
        // Consider throwing an exception or handling this case as needed
        return phoneNumber;
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

        if (!contacts.Any()) 
        {
            AnsiConsole.MarkupLine("No contacts found. Add some!");
            return null;
        }

        ContactDTO titles = new ContactDTO
        {
            Name = "_NAME_".PadRight(20),
            PhoneNumber = "_PHONE NUMBER_".PadRight(15),
            Email = "_EMAIL_".PadRight(25),
            ContactGroupName = "_GROUP_".PadRight(15)
        };

        var selectOptions = new SelectionPrompt<ContactDTO>();
        selectOptions.AddChoice(titles); // Hack-ish way to simulate a title
        selectOptions.AddChoices(contacts);
        selectOptions.AddChoice(new ContactDTO { Name = " " }); // Use what would otherwise be an invalid email to identify the "cancel" button

        selectOptions.UseConverter(contact =>
            (contact.Name == " " ? "EXIT".PadRight(20) :
            $"{contact.Name.PadRight(20)}") + (contact.ContactGroupName != null ? $" {contact.ContactGroupName.PadRight(15)}" : ""));

        selectOptions.Title("Select [green]Contact[/]");
        selectOptions.MoreChoicesText("Keep scrolling for more");

        ContactDTO selectedContact = AnsiConsole.Prompt(selectOptions);

        if (selectedContact == titles)
        {
            return SelectContact(contacts);
        }
        else
        {
            return selectedContact;
        }
    }

    public ContactGroupDTO SelectGroup(List<ContactGroupDTO> contactGroups)
    {
        AnsiConsole.Clear();
        var selectOptions = new SelectionPrompt<ContactGroupDTO>();

        selectOptions.AddChoices(contactGroups);
        selectOptions.AddChoice(new ContactGroupDTO { Name = " " }); // invalid group name is used to identfy the cancel button

        selectOptions.UseConverter(group => (group.Name == " " ? "EXIT" : $"{group.Name}")); // if email is 0 it's a cancel button 
        selectOptions.Title("Select [green]Group[/]");
        selectOptions.MoreChoicesText("Keep scrolling for more");

        ContactGroupDTO selectedGroup = AnsiConsole.Prompt(selectOptions);

        return selectedGroup;
    }
}
