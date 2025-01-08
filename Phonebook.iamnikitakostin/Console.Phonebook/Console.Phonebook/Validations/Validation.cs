using System.Text.RegularExpressions;
using Console.Phonebook.Controllers;

namespace Console.Phonebook.Validations;

internal class Validation: ConsoleController
{
    internal static bool Email(string emailAddress)
    {
        if (emailAddress == null)
        {
            ErrorMessage("Email cannot be empty");
            return false;
        }
        else if (emailAddress.Length < 4)
        {
            ErrorMessage("Email cannot have less than 5 characters");
            return false;
        }
        else if (!emailAddress.Contains('@'))
        {
            ErrorMessage("You must have a @ in the email");
            return false;
        }
        else if (!Regex.IsMatch(emailAddress, "^[a-zA-Z0-9._%+-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,}$")) { 
            ErrorMessage("Your domain is incorrect. It should have the following format '@abc.com'");
            return false;
        }
        return true;
    }
    internal static bool PhoneNumber(string phone)
    {
        if (phone == null)
        {
            ErrorMessage("Phone Number cannot be empty");
            return false;
        }
        else if (phone.Length < 6)
        {
            ErrorMessage("Phone Number cannot have less than 7 characters");
            return false;
        }
        else if (!phone.StartsWith('+'))
        {
            ErrorMessage("You must start a Phone Number with a +");
            return false;
        } else if (!Regex.IsMatch(phone, "^\\+[0-9]+$"))
        {
            ErrorMessage("The format of the phone number is incorrect.");
            return false;
        }
        return true;
    }
}
