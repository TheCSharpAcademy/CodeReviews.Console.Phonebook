using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook.Controllers
{
    public class ContactController
    {   
        public static void Add(Contact contact)
        {
            try
            {
                using var context = new ContactContext();
                context.Entry(contact.Category).State = EntityState.Unchanged;
                context.Contacts.Add(contact);
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Handle the exception
                Console.WriteLine($"Error adding contact: {ex.InnerException?.Message}");
            }
        }

        internal static List<Contact> GetContacts()
        {
            using var context = new ContactContext();
            var contacts = context.Contacts.ToList();
            return contacts;
        }
    }
}