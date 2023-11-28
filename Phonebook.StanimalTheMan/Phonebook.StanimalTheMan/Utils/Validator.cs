using System.Text.RegularExpressions;

namespace Phonebook.StanimalTheMan.Utils
{
    internal class Validator
    {
        readonly static string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        readonly static string phoneNumberPattern = @"^\d{3}-\d{3}-\d{4}$";

        internal static String GetEmail(string emailInput)
        {
            while (!Regex.IsMatch(emailInput, emailPattern))
            {
                Console.WriteLine($"\n\nNot a valid email.  Please enter email with the format: {emailPattern}.\n\n");
                emailInput = Console.ReadLine();
            }

            return emailInput;
        }

        internal static String GetPhoneNumber(string phoneNumberInput)
        {
            while (!Regex.IsMatch(phoneNumberInput, phoneNumberPattern))
            {
                Console.WriteLine("\n\nNot a valid phone number.  Please enter phone number with the format: XXX-XXX-XXXX");
                phoneNumberInput = Console.ReadLine();
            }

            return phoneNumberInput;
        }
    }
}
