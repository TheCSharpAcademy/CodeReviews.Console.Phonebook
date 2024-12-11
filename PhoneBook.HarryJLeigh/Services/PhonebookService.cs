using Phonebook.Data;
using Phonebook.Models;
using Phonebook.Utilities;
using Phonebook.Views;
using Spectre.Console;

namespace Phonebook.Services;

public class PhonebookService
{
    private readonly AppDbContext _context  = new AppDbContext();
    
    internal void CreateContact(Contact contact)
    {
        _context.Contacts.Add(contact);
        _context.SaveChanges();
    }

    internal void UpdateContact(Contact contact)
    {
        _context.SaveChanges();
    }

    internal void DeleteContact(int id)
    {
        var contact = _context.Contacts.Find(id);
        _context.Contacts.Remove(contact);
        _context.SaveChanges();
    }
    
    internal List<int> GetContactsId() => _context.Contacts.Select(c => c.Id).ToList();
    
    internal List<Contact> GetAllContacts() => _context.Contacts.ToList();

    internal Contact GetContactById(int id) => _context.Contacts.FirstOrDefault(c => c.Id == id);

    internal List<Contact> GetContactsByCategory(string category) => 
        _context.Contacts.Where(c => c.Category == category).ToList();
}