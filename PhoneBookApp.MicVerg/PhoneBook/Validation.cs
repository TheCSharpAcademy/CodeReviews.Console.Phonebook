using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneBook
{
    internal class Validation
    {
        internal static bool IsValidMail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, emailPattern);
        }
        internal static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Allow the format: 0475/35.12.69
            string phonePattern = @"^\d{4}/\d{2}\.\d{2}\.\d{2}$";
            return Regex.IsMatch(phoneNumber, phonePattern);
        }
    }
}
