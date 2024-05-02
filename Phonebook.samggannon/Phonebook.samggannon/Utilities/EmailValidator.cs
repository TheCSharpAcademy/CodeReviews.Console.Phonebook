using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Phonebook.samggannon.Utilities
{
    internal static class EmailValidator
    {
        public static bool IsEmailValid(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            return email.Contains("@");
        }
    }

    internal static class PhoneNumberValidator
    {
        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber))
                return false;

            return phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit);
        }
    }
}
