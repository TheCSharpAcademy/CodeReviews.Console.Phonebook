using System.Text.RegularExpressions;

namespace Phonebook
{
    internal static class Helpers
    {
        internal static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            return emailRegex.IsMatch(email);
        }

        internal static bool IsValidPhoneNum(string phoneNum)
        {
            return Regex.Match(phoneNum, @"^\+?1?\d{10}$").Success;
        }
    }
}