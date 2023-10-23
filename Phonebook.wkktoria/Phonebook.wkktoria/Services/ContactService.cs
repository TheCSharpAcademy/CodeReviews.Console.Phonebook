using Phonebook.wkktoria.Models;

namespace Phonebook.wkktoria.Services;

public class ContactService
{
    private readonly AppDbContext _db = new();

    public void AddContact(Contact contact)
    {
        try
        {
            _db.Add(contact);
            _db.SaveChanges();
        }
        catch (Exception)
        {
            Outputs.ExceptionMessage("Failed to add contact to database.");
        }
    }

    public void UpdateContact(Contact contact)
    {
        try
        {
            _db.Update(contact);
            _db.SaveChanges();
        }
        catch (Exception)
        {
            Outputs.ExceptionMessage("Failed to update contact.");
        }
    }

    public void RemoveContact(Contact contact)
    {
        try
        {
            _db.Remove(contact);
            _db.SaveChanges();
        }
        catch (Exception)
        {
            Outputs.ExceptionMessage("Failed to remove contact from database.");
        }
    }

    public Contact? GetContactById(int id)
    {
        try
        {
            var contact = _db.Contacts.SingleOrDefault(c => c.Id == id);

            return contact;
        }
        catch (Exception)
        {
            Outputs.ExceptionMessage("Failed to get contact from database.");
        }

        return null;
    }

    public List<Contact> GetContacts()
    {
        try
        {
            var contacts = _db.Contacts.ToList();

            return contacts;
        }
        catch (Exception)
        {
            Outputs.ExceptionMessage("Failed to get contacts from database.");
        }

        return new List<Contact>();
    }
}