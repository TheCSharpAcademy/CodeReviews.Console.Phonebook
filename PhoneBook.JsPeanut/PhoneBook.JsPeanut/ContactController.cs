using PhoneBook.JsPeanut.Models;

namespace PhoneBook.JsPeanut
{
    internal class ContactController
    {
        internal static void AddContact(string name, string phoneNumber)
        {
            using var db = new ContactsContext();

            db.Add(new Contact { Name = name, PhoneNumber = phoneNumber });

            db.SaveChanges();
        }

        internal static void DeleteContact(Contact contact)
        {
            using var db = new ContactsContext();

            db.Remove(contact);

            db.SaveChanges();
        }

        internal static void UpdateContact(Contact contact)
        {
            using var db = new ContactsContext();

            db.Update(contact);

            db.SaveChanges();
        }

        internal static Contact GetContactById(int id)
        {
            using var db = new ContactsContext();
            var contact = db.Contacts.SingleOrDefault(x => x.Id == id);

            return contact;
        }

        internal static List<Contact> GetContacts()
        {
            using var db = new ContactsContext();

            var contacts = db.Contacts.ToList();
            
            return contacts;
        }
    }
}
