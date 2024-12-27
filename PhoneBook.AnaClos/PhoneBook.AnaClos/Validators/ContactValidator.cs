using System.Text.RegularExpressions;

namespace PhoneBook.AnaClos.Validators
{
    public class ContactValidator
    {
        public bool EmailValidator(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public bool PhoneValidator(string phoneNumber)
        {
            string pattern = @"^\d{10,15}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}