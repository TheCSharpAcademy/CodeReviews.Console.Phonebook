using Phonebook.wkktoria.Models;

namespace Phonebook.wkktoria.Services;

public class ContactService
{
    private readonly AppDbContext _db = new();

    public void AddContact(Contact contact)
    {
        _db.Add(contact);
        _db.SaveChanges();
    }

    public void UpdateContact(Contact contact)
    {
        _db.Update(contact);
        _db.SaveChanges();
    }

    public void RemoveContact(Contact contact)
    {
        _db.Remove(contact);
        _db.SaveChanges();
    }

    public Contact? GetContactById(int id)
    {
        var contact = _db.Contacts.SingleOrDefault(c => c.Id == id);

        return contact;
    }

    public List<Contact> GetContacts()
    {
        var contacts = _db.Contacts.ToList();

        return contacts;
    }
}