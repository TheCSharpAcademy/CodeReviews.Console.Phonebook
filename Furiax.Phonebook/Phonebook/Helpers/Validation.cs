using System.Text.RegularExpressions;

namespace Phonebook.Helpers
{
    internal class Validation
    {
        internal static bool IsValidPhoneNumber(string phoneNumber)
        {
            foreach (char c in phoneNumber)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        internal static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, pattern);
        }

    }
}
