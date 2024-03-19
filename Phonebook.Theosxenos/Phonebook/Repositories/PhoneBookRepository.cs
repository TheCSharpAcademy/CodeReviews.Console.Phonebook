using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook.Repositories;

public class PhoneBookRepository
{
    public void AddContact(Contact contact)
    {
        try
        {
            using var context = new PhoneBookContext();
            context.Contacts.Add(contact);
            context.SaveChanges();
        }
        catch (Exception e) when (e.InnerException is SqlException { Number: 2601 })
        {
            throw new ArgumentException($"Contact with name {contact.Name} already exists.");
        }
    }

    public void UpdateContact(Contact contact)
    {
        try
        {
            using var context = new PhoneBookContext();
            context.Contacts.Update(contact);
            context.SaveChanges();
        }
        catch (Exception e) when (e.InnerException is SqlException { Number: 2601 })
        {
            throw new ArgumentException($"Contact with name {contact.Name} already exists.");
        }
    }

    public List<Contact> GetAll()
    {
        using var context = new PhoneBookContext();
        return context.Contacts.AsNoTracking().ToList();
    }

    public void DeleteContact(Contact contact)
    {
        using var context = new PhoneBookContext();
        context.Contacts.Remove(contact);
        context.SaveChanges();
    }
}