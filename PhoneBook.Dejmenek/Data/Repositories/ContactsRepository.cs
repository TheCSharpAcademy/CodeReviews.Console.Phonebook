using Microsoft.EntityFrameworkCore;
using PhoneBook.Dejmenek.Data.Intefaces;
using PhoneBook.Dejmenek.Models;

namespace PhoneBook.Dejmenek.Data.Repositories;
public class ContactsRepository : IContactsRepository
{
    private readonly PhoneBookContext _context;

    public ContactsRepository(PhoneBookContext context)
    {
        _context = context;
    }

    public void AddContact(Contact contact)
    {
        _context.Contacts.Add(contact);
        _context.SaveChanges();
    }

    public void DeleteContact(int id)
    {
        var contact = _context.Contacts.Find(id);
        _context.Contacts.Remove(contact);
        _context.SaveChanges();
    }

    public Contact GetContact(int id)
    {
        return _context.Contacts.Find(id);
    }

    public List<Contact> GetAllContacts()
    {
        return _context.Contacts.Include(c => c.Category).ToList();
    }

    public List<Contact> GetContactsByCategory(int categoryId)
    {
        return _context.Contacts.Include(c => c.Category).Where(c => c.CategoryId == categoryId).ToList();
    }

    public void UpdateContact(Contact contact)
    {
        _context.Contacts.Update(contact);
        _context.SaveChanges();
    }

    public bool PhoneNumberExists(string phoneNumber)
    {
        return _context.Contacts.Any(c => c.PhoneNumber == phoneNumber);
    }
}
