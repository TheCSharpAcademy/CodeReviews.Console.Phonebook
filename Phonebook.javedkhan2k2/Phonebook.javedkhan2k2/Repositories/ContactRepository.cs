using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Entities;
using Phonebook.Repositories.Interfaces;

namespace Phonebook.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly PhonebookDbContext _context;

    public ContactRepository(PhonebookDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Contact> GetAllContacts() => _context.Contacts.Include( c => c.ContactCategory).ToList();

    public Contact? GetContactById(int id) => _context.Contacts.Find(id);

    public void AddContact(Contact contact)
    {
       _context.Contacts.Add(contact);
       _context.SaveChanges();
    }

    public void DeleteContact(Contact contact)
    {
        _context.Contacts.Remove(contact);
        _context.SaveChanges();
    }
    
    public void UpdateContact(Contact contact)
    {
        _context.Contacts.Update(contact);
        _context.SaveChanges();
    }

}