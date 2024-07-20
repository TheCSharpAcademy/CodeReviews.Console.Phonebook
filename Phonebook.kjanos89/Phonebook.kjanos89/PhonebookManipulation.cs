using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.kjanos89
{
    public class PhonebookManipulation
    {
        public void AddContact(Contact contact)
        {
            using (var context = new PhonebookContext())
            {
                context.Contacts.Add(contact);
                context.SaveChanges();
            }
        }
        public List<Contact> GetContacts()
        {
            using (var context = new PhonebookContext())
            {
                return context.Contacts.ToList();
            }
        }
        public void UpdateContact(Contact contact)
        {
            using (var context = new PhonebookContext())
            {
                context.Contacts.Update(contact);
                context.SaveChanges();
            }
        }
        public void DeleteContact(int id)
        {
            using(var context = new PhonebookContext())
            {
                var contact = context.Contacts.Find(id);
                if (contact != null)
                {
                    context.Contacts.Remove(contact);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("No contact found with the id. Try again:");
                    DeleteContact(Int32.Parse(Console.ReadLine()));
                }
            }
        }

    }
}
