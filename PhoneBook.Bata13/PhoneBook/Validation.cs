using Spectre.Console;

namespace PhoneBook;
internal class Validation
{
    internal static string GetValidPhoneNumberFromUser()
    {
        string contactPhoneNumber;

        do
        {
            contactPhoneNumber = AnsiConsole.Ask<string>("Contact's PhoneNumber:");
            if (!IsValidPhoneNumber(contactPhoneNumber))
            {
                Console.WriteLine(@$"
                    Invalid PhoneNumber format. Please enter a valid PhoneNumber.
                    Enter 10 digit number without any space or - or _");
            }
        } while (!IsValidPhoneNumber(contactPhoneNumber));

        return contactPhoneNumber;
    }
    internal static bool IsValidPhoneNumber(string phoneNumber)
    {
        return phoneNumber.Length == 10 && long.TryParse(phoneNumber, out _);
    }
    internal static string GetValidEmailFromUser()
    {
        string contactEmail;

        do
        {
            contactEmail = AnsiConsole.Ask<string>("Contact's Email:");
            if (!isValidEmail(contactEmail))
            {
                Console.WriteLine("Invalid email format. Please enter a valid email address.");
            }
        } while (!isValidEmail(contactEmail));

        return contactEmail;
    }

    internal static bool isValidEmail(string contactEmail)
    {
        return contactEmail.Contains("@");
    }
    internal static string GetValidEmailToUpdateFromUser(string contactEmail)
    {        
        do
        {
            if (!isValidEmail(contactEmail))
            {
                Console.WriteLine("Invalid email format. Please enter a valid email address.");
                contactEmail = AnsiConsole.Ask<string>("Contact's Email:");
            }
            
        } while (!isValidEmail(contactEmail));

        return contactEmail;
    }
    internal static string GetValidPhoneNumberToUpdateFromUser(string contactPhoneNumber)
    {        
        do
        {            
            if (!IsValidPhoneNumber(contactPhoneNumber))
            {
                Console.WriteLine(@$"       
                    Invalid PhoneNumber format. 
                    Please enter a valid PhoneNumber. 
                    Enter 10 digit number without any space or - or _");
                contactPhoneNumber = AnsiConsole.Ask<string>("Contact's PhoneNumber:");
            }
            
        } while (!IsValidPhoneNumber(contactPhoneNumber));

        return contactPhoneNumber;
    }
}
