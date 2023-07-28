using Phonebook.Models;

namespace Phonebook.DataAccess
{
    public class DataAccessor
    {
        public static ContactContext AddContact(string name, string phoneNumber)
        {
            using var db = new ContactContext();
            db.Add(new Contact { Name = name, PhoneNumber = phoneNumber });
            db.SaveChanges();
            return db;
        }

        public static void DeleteContact(string name)
        {
            using var db = new ContactContext();
            db.Remove(db.Contacts.Where(c => c.Name == name).FirstOrDefault());
            db.SaveChanges();
        }

        public static List<Contact> GetContacts()
        {
            using var db = new ContactContext();
            return db.Contacts.OrderBy(x => x.Name).ToList();
        }

        public static Contact GetContact(string name)
        {
            using var db = new ContactContext();
            return db.Contacts.Where(c => c.Name == name).FirstOrDefault();
        }

        public static void UpdateContactName(Contact contact, string newName)
        {
            using var db = new ContactContext();
            contact.Name = newName;
            var entry = db.Entry(contact);
            entry.Property(e => e.Name).IsModified = true;
            db.SaveChanges();
        }

        public static void UpdateContactPhoneNumber(Contact contact, string newNum)
        {
            using var db = new ContactContext();
            contact.PhoneNumber = newNum;
            var entry = db.Entry(contact);
            entry.Property(e => e.PhoneNumber).IsModified = true;
            db.SaveChanges();
        }
    }
}
