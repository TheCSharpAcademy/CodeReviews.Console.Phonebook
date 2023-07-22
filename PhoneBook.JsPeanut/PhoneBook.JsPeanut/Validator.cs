using PhoneBook.JsPeanut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.JsPeanut
{
    internal class Validator
    {
        public static string CheckName(string name)
        {
            List<Contact> contacts = ContactController.GetContacts();

            if (string.IsNullOrEmpty(name)) return "null/empty";

            if (contacts.Any(c => c.Name == name))
            {
                return "duplicated name";
            }

            return "valid";
        }

        public static string CheckNumber(string number)
        {
            List<Contact> contacts = ContactController.GetContacts();

            if (string.IsNullOrEmpty(number)) return "null/empty";

            if (!Int32.TryParse(number, out _) || !long.TryParse(number, out _))
            {
                return "not a number";
            }

            if (contacts.Any(c => c.PhoneNumber == number))
            {
                return "duplicated number";
            }

            return "valid";
        }
    }
}
