using System.Text.RegularExpressions;

namespace jollejonas.Phonebook.Validation;

public class InputValidation
{
    public static bool IsValidName(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        return true;        
    }
    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber)) return false;
        
        string phoneNumberRegex = @"^(\+[0-9]{1,3})?[0-9]{8,12}$";

        if(!Regex.IsMatch(phoneNumber, phoneNumberRegex))
        {
            Console.WriteLine("The phone number format is invalid. Please enter a valid number.");
            return false;
        }
        return true;
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email)) return false;
        
        string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        if(!Regex.IsMatch(email, emailRegex))
        {
            Console.WriteLine("The email format is invalid. Please enter a valid email.");
            return false;
        }
        return true;
    }

    public static bool IsValidNote(string note)
    {
        if (string.IsNullOrEmpty(note) || note.Length > 200) return false;
        return true;
    }
}
