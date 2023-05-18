using Phonebook.Data;
using Phonebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook
{
    internal class UserInput
    {
        public static void AddContact()
        {
            using var db = new ContactContext();
            Console.WriteLine("Please enter name of new contact.");
            var name = Console.ReadLine();

            Console.WriteLine("Please enter phone number for new contact.");
            var number = Console.ReadLine().ToString();

            db.Add(new Contact { Name = name, PhoneNumber = number });
            db.SaveChanges();
        }
        public static void ViewContacts()
        {
            using var db = new ContactContext();

            var contacts = db.Contacts
                .OrderBy(x => x.Name);

            List<Contact> contact = new List<Contact>();

            foreach (Contact x in contacts)
            {
                contact.Add(new Contact
                {
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber,
                });
            }
            TableDesigner.DisplayTable(contact);
        }
        public static void DeleteContact()
        {

            ViewContacts();
            Console.WriteLine("\nPlease select name of person you wish to delete.");
            var name = Console.ReadLine();
            using (var db = new ContactContext())
            {
                var deleteName = db.Contacts.FirstOrDefault(x => x.Name == name);
                if (deleteName != null)
                {
                    db.Contacts.Remove(deleteName);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"{name} does not exist in phonebook. Please try again.");
                    name = Console.ReadLine();
                }
                
            }


        }
    }
}
