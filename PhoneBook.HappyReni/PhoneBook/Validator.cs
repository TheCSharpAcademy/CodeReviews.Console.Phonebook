using System.Text.RegularExpressions;

namespace PhoneBook
{
    internal static class Validation
    {

        public static string CheckEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (Regex.IsMatch(email, pattern)) return email;
            else throw new Exception($"Invalid Email input.");
        }
        public static string CheckPhoneNumber(string email)
        {
            string pattern = @"^[0-9]{3}-[0-9]{4}-[0-9]{4}$";
            if (Regex.IsMatch(email, pattern)) return email;
            else throw new Exception($"Invalid Phone Number input.");
        }
    }
}