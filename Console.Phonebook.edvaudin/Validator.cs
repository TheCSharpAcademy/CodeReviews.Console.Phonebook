using Microsoft.IdentityModel.Tokens;
using Phonebook.DataAccess;

namespace Phonebook
{
    internal class Validator
    {
        public static bool IsValidOption(string? input)
        {
            string[] validOptions = { "v", "a", "d", "u", "0" };
            foreach (string validOption in validOptions)
            {
                if (input == validOption)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsValidStringInput(string? input, int maxLength) => !input.IsNullOrEmpty() && input.Length > 0 && input.Length <= maxLength;

        public static bool IsValidPhoneNumber(string? input) => !input.IsNullOrEmpty() && input.Length > 0 && input.Length <= 30 && input.All(c => char.IsDigit(c));

        public static bool IsNameInContacts(string name)
        {
            using var db = new ContactContext();
            return db.Contacts.Any(c => c.Name == name);
        }

        internal static bool IsValidUpdateOption(string? input) => input == "n" || input == "p";
    }
}
