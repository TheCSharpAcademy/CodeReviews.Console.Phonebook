using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console.Phonebook.Data;
using Console.Phonebook.Models;
using Spectre.Console;

namespace Console.Phonebook.Services;
internal class ContactService
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
            AnsiConsole.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            var contact = GetById(id);
            _context.Contacts.Remove(contact);
            return true;
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"An error occurred: {ex.Message}");
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
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }
}
