using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook.Repositories;

public class PhoneBookRepository
{
    private readonly PhoneBookContext context = new();

    public void AddContact(Contact contact)
    {
        if (context.Contacts.Any(c => c.Name == contact.Name))
            throw new ArgumentException($"Contact with name {contact.Name} already exists.");

        context.Contacts.Add(contact);
        context.SaveChanges();
    }

    public void UpdateContact(Contact contact)
    {
        if (context.Contacts.Any(c => c.Name == contact.Name))
            throw new ArgumentException($"Contact with name {contact.Name} already exists.");

        context.Contacts.Update(contact);
        context.SaveChanges();
    }

    public List<Contact> GetAll()
    {
        return context.Contacts.AsNoTracking().ToList();
    }

    public void DeleteContact(Contact contact)
    {
        context.Contacts.Remove(contact);
        context.SaveChanges();
    }
}