using PhoneBook.mefdev.Models;
using Spectre.Console;

namespace PhoneBook.mefdev.Service
{
	internal class ContactService: PhoneBookService
	{
        public ContactService()
        {

        }

        public void AddContact(Contact contact)
        {
            try
            {
                _db.Add(contact);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Invalid operation: {ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        public Contact GetContact(int id)
        {
            var contact = _db.Contacts.OrderBy(c => c.Id == id).FirstOrDefault();
            if (contact == null)
            {
                return null;
            }
            return contact;
        }

        public List<Contact> GetContacts()
        {
            try
            {
                var contacts = _db.Contacts.OrderBy(c => c.Id).ToList();
                if (contacts == null || contacts.Count() <= 0)
                {
                    return null;
                }
                return contacts;
            }
            catch (Exception ex)
            {
                throw new Exception($"Invalid operation: {ex.Message}");
            }
        }

        public Contact? GetContactByName(string name)
        {
            try
            {
                return _db.Contacts.FirstOrDefault(c => c.Name == name);
            }
            catch (Exception ex)
            {
                throw new Exception($"Invalid operation: {ex.Message}");
            }
        }

        public void UpdateContact(Contact contact, Contact newContact)
        {
            try
            {
                if (contact != null)
                {
                    contact.Name = newContact.Name;
                    contact.Phone = newContact.Phone;
                    contact.Email = newContact.Email;
                    _db.Update(contact);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Invalid operation: {ex.Message}");
            }
        }

        public bool DeleteContactByName(string name)
        {
            try
            {
                var contact = GetContactByName(name);
                if (contact == null)
                {
                    AnsiConsole.MarkupLine("[red]A contact cannot be deleted[/]");
                    return false;
                }
                _db.Remove(contact);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Invalid operation: {ex.Message}");
            }
        }
    }
}