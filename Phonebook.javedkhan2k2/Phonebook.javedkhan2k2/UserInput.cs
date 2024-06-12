
using Phonebook.Entities;
using Spectre.Console;
using Phonebook.Validators;

namespace Phonebook;

public class UserInput
{
    private static readonly string _nameMessage = "\n\nYour [green]Name[/] Should Start with a [maroon]Alphabet[/].\nYour [green]Name[/] Should be at least [yellow]3 charcters long[/].\nYour [green]Name[/] can contain white spaces.\nPlease Enter Your [green]Name[/]:";
    private static readonly string _emailMessage = "\n\nPlease Enter a Valid [green]Email Address[/]:";
    public static readonly string _phoneNumber = "\n\nYour [green]Phone Number[/] Should be in the Format [maroon]0XXXXXXXXXXX[/].\nYour [green]Phone Number[/] Should be [yellow]12 Digits long[/].\nPlease Enter Your [green]Phone Number[/]:";
    
    internal static Contact GetNewContact()
    {
        AnsiConsole.Clear();
        Contact contact = new Contact();
        contact.Name = UserInput.GetStringInput(_nameMessage);
        contact.Email = UserInput.GetEmailInput(_emailMessage);
        contact.PhoneNumber = UserInput.GetPhoneInput(_phoneNumber);
        return contact;
    }
    
    internal static void UpdateContact(Contact contact)
    {
        contact.Name = AnsiConsole.Confirm("Do you want to Update Name") ? GetStringInput(_nameMessage) : contact.Name;
        contact.Email = AnsiConsole.Confirm("Do you want to Update Email") ? GetEmailInput(_emailMessage) : contact.Email;
        contact.PhoneNumber = AnsiConsole.Confirm("Do you want to Update Phone Number") ? GetPhoneInput(_phoneNumber) : contact.PhoneNumber;
    }

    public static string GetStringInput(string message)
    {
        string name = AnsiConsole.Ask<string>(message).Trim();
        while(!ValidatorHelper.IsValidName(name))
        {
            name = AnsiConsole.Ask<string>($"Invalid name [maroon]{name}[/] entered. {message}").Trim();
        }
        return name;
    }

    internal static string GetEmailInput(string message)
    {
        string email = AnsiConsole.Ask<string>(message).Trim();
        while(!ValidatorHelper.IsValidEmail(email))
        {
            email = AnsiConsole.Ask<string>($"Invalid email [maroon]{email}[/] entered. {message}").Trim();
        }
        return email;
    }

    internal static string GetPhoneInput(string message)
    {
        string phone = AnsiConsole.Ask<string>(message).Trim();
        while(!ValidatorHelper.IsValidPhoneNumber(phone))
        {
            phone = AnsiConsole.Ask<string>($"Invalid phone number [maroon]{phone}[/] entered. {message}").Trim();
        }
        return phone;
    }

    internal static int GetIntInput()
    {
        int id = AnsiConsole.Ask<int>("Enter contact Id from the table");
        return id;
    }

}