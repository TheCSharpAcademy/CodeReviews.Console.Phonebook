using hasona23.PhoneBook.Models;

namespace hasona23.PhoneBook.Database
{
    public static class ContactsDB
    {
        public static void Initialise()
        {
            using (var context = new PhoneBookContext())
            {
                context.EnsureDatabaseCreated();
            }
        }
        public static void Update(Contact oldContact, Contact newContact)
        {
            using (var context = new PhoneBookContext())
            {
                context.UpdateContact(oldContact, newContact);
            }
        }
        public static void Delete(Contact contact)
        {
            using (var context = new PhoneBookContext())
            {
                context.DeleteContact(contact);
            }
        }
        public static List<Contact> GetAllContacts()
        {
            using (var context = new PhoneBookContext())
            {
                return context.GetAllContacts();
            }
        }
        public static List<Contact> GetContacts(Contact filterContact)
        {
            using (var context = new PhoneBookContext())
            {
                return context.GetContacts(filterContact);
            }
        }
        public static void AddContact(Contact contact)
        {
            using (var context = new PhoneBookContext())
            {
                context.AddContact(contact);
            }
        }
    }
}
