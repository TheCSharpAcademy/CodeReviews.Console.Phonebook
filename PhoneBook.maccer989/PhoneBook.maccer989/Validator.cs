using PhoneBook.maccer989.DataAccess;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PhoneBook.maccer989
{
    public class Validator
    {
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\+\d{1,4}\d{1,14}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
        public static bool IsValidEmailAddress(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
        public static bool IsStringValid(string stringInput)
        {
            if (String.IsNullOrEmpty(stringInput))
            {
                return false;
            }

            foreach (char c in stringInput)
            {
                if (!Char.IsLetter(c) && c != '/' && c != ' ')
                    return false;
            }
            return true;
        }

        public static bool IsIdValid(string stringInput)
        {
            bool output = false;
            int user;

            if (Int32.TryParse(stringInput, out int result))
            {
                using (var db = new ContactContext())
                {
                    user = db.Contacts.Where(c => c.Id == result).Count();
                }
                if ((result!=0) & user !=0)
                {
                    output = true;                    
                }               
            }
            return output;
        }
    }           
}
