using PhoneBook.Models;
using PhoneBook.Services;

namespace PhoneBook
{
    public static class PhoneBookController
    {
        public static void InsertContact()
        {
            string? name = UI.GetContactName();
            string? email = UI.GetContactEmail();
            string? phone = UI.GetContactPhone();
            using var db = new PhoneBookContext();
            db.Add(new Contact{ Name = name, Email = email, PhoneNumber = phone });
            db.SaveChanges();

        }
        public static void RemoveContact() 
        {
            using var db = new PhoneBookContext();
            var option = UI.GetContactOptionInput();
            db.Remove(option);
            db.SaveChanges();
        }
        public static void UpdateContact()
        {
            using var db = new PhoneBookContext();
            var contactOption = PhoneBookService.UpdateContact();
            db.Update(contactOption);
            db.SaveChanges();
        }
        public static void ViewContacts()
        {
            UI.ShowContactTable(GetContactsList());
        }
        internal static Contact GetContactById(int id)
        {
            using var db = new PhoneBookContext();
            var contact = db.Contacts.SingleOrDefault(x => x.Id == id);
            return contact;
        }
        internal static List<Contact> GetContactsList()
        {
            using var db = new PhoneBookContext();
            var contacts = db.Contacts.ToList();
            return contacts;
        }
    }
}
