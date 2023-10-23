using PhoneBook.DTOs;
using PhoneBook.Models;

namespace PhoneBook.Data
{
    public class PhoneBookRepository
    {
        private readonly PhoneBookDbContext _context;

        public PhoneBookRepository(PhoneBookDbContext context)
        {
            _context = context;
        }

        public List<Contact> GetContacts()
        {
            return _context.Contacts.ToList();
        }

        public Contact? GetContact(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null) return null;

            return contact;
        }

        public Contact? AddContact(ContactDTO newContact)
        {
            if (newContact == null) return null;

            var contact = new Contact()
            {
                Name = newContact.Name,
                Email = newContact.Email,
                PhoneNumber = newContact.PhoneNumber,
            };

            _context.Contacts.Add(contact);

            _context.SaveChanges();

            return contact;
        }

        public Contact? UpdateContact(int id, Contact updatedContact)
        {
            if (id != updatedContact.Id) return null;

            var contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null) return null;

            contact.Name = updatedContact.Name;
            contact.Email = updatedContact.Email;
            contact.PhoneNumber = updatedContact.PhoneNumber;

            _context.Update(contact);
            _context.SaveChanges();

            return contact;
        }


        public Contact? DeleteContact(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null) return null;

            _context.Contacts.Remove(contact);
            _context.SaveChanges();

            return contact;
        }
    }
}
