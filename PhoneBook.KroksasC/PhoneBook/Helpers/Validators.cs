using System.Text.RegularExpressions;

namespace PhoneBook.Helpers
{
    internal class Validators
    {
        public static bool ValidateNumber(string number)
        {
            return !string.IsNullOrEmpty(number) && number.All(char.IsDigit);
        }
        public static bool ValidateEmail(string email)
        {
            string pattern = @"[\w]*@*[a-z]*\.*[\w]{5,}(\.)*(com)*(@gmail\.com)";
            return Regex.IsMatch(email, pattern);
        }
        public static bool ValidateName(string name) 
        {
            return name.All(s => char.IsLetter(s)) && !string.IsNullOrEmpty(name);
        
        }
    }
}
