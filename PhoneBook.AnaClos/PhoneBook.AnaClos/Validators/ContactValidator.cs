using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            string pattern = @"^\d{10}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
