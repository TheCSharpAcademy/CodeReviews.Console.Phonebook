using PhoneBook.DouglasFir.Data;
using PhoneBook.DouglasFir.Models;

namespace PhoneBook.DouglasFir.Services;

public class ContactService
{
    private readonly PhoneBookContext _context;

    public ContactService(PhoneBookContext context)
    {
        _context = context;
    }

    public IEnumerable<Contact> GetAllContacts()
    {
        return _context.Contacts.ToList();
    }

    public void AddContact(Contact contact)
    {
        _context.Contacts.Add(contact);
        _context.SaveChanges();
    }

    public void UpdateContact(Contact contact)
    {
        _context.Contacts.Update(contact);
        _context.SaveChanges();
    }

    public void DeleteContact(int id)
    {
        var contact = _context.Contacts.Find(id);

        if (contact == null)
        {
            throw new KeyNotFoundException("Contact not found.");
        }

        _context.Contacts.Remove(contact);
        _context.SaveChanges();
    }

    public Contact? GetContactById(int id)
    {
        var contact = _context.Contacts.Find(id);

        if (contact == null)
        {
            throw new KeyNotFoundException("Contact not found.");
        }

        return contact;
    }
}