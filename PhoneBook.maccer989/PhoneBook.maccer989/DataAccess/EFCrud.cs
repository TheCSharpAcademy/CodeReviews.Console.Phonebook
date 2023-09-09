using PhoneBook.maccer989.DataAccess;
using PhoneBook.maccer989.Models;
using System;
using System.Linq;

namespace PhoneBook.maccer989
{
    public class EFCrud
    {
        public static bool CheckForContacts()
        {
            bool output = false;
            using (var db = new ContactContext())
            {
                var records = db.Contacts
                    .ToList();
                if (records.Count > 0)
                     output = true;                         
                return output;
            }
        }
        public static void RemoveUser(int id)
        {
            using (var db = new ContactContext())
            {
                var user = db.Contacts
                    .Where(c => c.Id == id).First();
                db.Contacts.Remove(user);
                db.SaveChanges();
            }
        }
        public static void UpdateContact(int id, string firstName, string lastName, string phoneNumber, string emailAddress)
        {
            using (var db = new ContactContext())
            {
                var user = db.Contacts.Where(c => c.Id == id).First();
                user.FirstName = firstName;
                user.LastName = lastName;
                user.EmailAddress = emailAddress;
                user.PhoneNumber = phoneNumber;
                db.SaveChanges();
            }
        }
        public static void CreateContact(string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            var c = new Contact
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber
            };
            using (var db = new ContactContext())
            {
                db.Contacts.Add(c);
                db.SaveChanges();
            }
        }
        public static void ReadAll()
        {
            using (var db = new ContactContext())
            {
                var records = db.Contacts
                    .ToList();

                foreach (var c in records)
                {
                    Console.WriteLine($"{ c.Id} { c.FirstName } { c.LastName } { c.EmailAddress } { c.PhoneNumber }");
                }
            }
        }
        public static void RemoveContactId(int id)
        {
            using (var db = new ContactContext())
            {
              var user = db.Contacts
              .Where(c => c.Id == id).First();
              db.Contacts.Remove(user);
              db.SaveChanges();
            }
        }
    }
}
