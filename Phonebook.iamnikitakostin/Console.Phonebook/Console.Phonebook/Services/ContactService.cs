using Console.Phonebook.Controllers;
using Console.Phonebook.Data;
using Console.Phonebook.Models;

namespace Console.Phonebook.Services;
internal class ContactService : ConsoleController
{
    private readonly DataContext _context;

    public ContactService(DataContext context)
    {
        _context = context;
    }

    public Contact? GetById(int id)
    {
        var contact = _context.Contacts
                            .FirstOrDefault(x => x.Id == id);
        return contact;
    }

    public List<Contact> GetAll()
    {
        var contacts = _context.Contacts.ToList();
        return contacts;
    }

    public bool Add(Contact contact)
    {
        try
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage($"An error occurred: {ex.Message}");
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            var contact = GetById(id);
            _context.Contacts.Remove(contact);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage($"An error occurred: {ex.Message}");
            return false;
        }
    }

    public bool Update(Contact contact)
    {
        try
        {
            var savedContact = GetById(contact.Id);
            savedContact.Email = contact.Email;
            savedContact.PhoneNumber = contact.PhoneNumber;
            savedContact.Name = contact.Name;
            savedContact.Category = contact.Category;
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage($"An error occurred: {ex.Message}");
            return false;
        }
    }
}
