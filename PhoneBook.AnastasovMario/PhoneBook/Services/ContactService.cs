using Microsoft.EntityFrameworkCore;
using PhoneBook.Contracts;
using PhoneBook.Data.Models;
using PhoneBook.Dtos;

namespace PhoneBook.Services
{
  public class ContactService(PhonebookDbContext context) : IContactService
  {
    private PhonebookDbContext _context = context;

    public void AddContact(ContactDto contact)
    {
      bool emailExists = _context.Contacts.FirstOrDefault(c => c.Email == contact.Email) != null;

      if (emailExists)
      {
        Console.WriteLine("The email is already in use");
      }

      bool phoneExists = _context.Contacts.FirstOrDefault(c => c.PhoneNumber == contact.PhoneNumber) != null;

      if (phoneExists)
      {
        Console.WriteLine("The number is already in use");
      }

      _context.Contacts.Add(new Contact
      {
        Name = contact.Name,
        PhoneNumber = contact.PhoneNumber,
        Email = contact.Email,
      });

      _context.SaveChanges();

    }

    public void DeleteContact(int id)
    {
      var contact = _context.Contacts.FirstOrDefault(c => c.Id == id);

      if (contact != null)
      {
        _context.Contacts.Remove(contact);
        _context.SaveChanges();

        Console.WriteLine("\nContact has been deleted successfully");
      }
      else
      {
        Console.WriteLine("\nContact with this Id doesn't exist");
      }
    }

    public List<Contact> GetAllContacts()
    {
      return [.. _context.Contacts];
    }

    public Contact GetContact(int id)
    {
      var contact = _context.Contacts.FirstOrDefault(c => c.Id == id);

      if (contact == null)
      {
        Console.WriteLine("Contact with this Id doesn't exist.\n");
      }

      return contact;
    }

    public void UpdateContact(int id, ContactDto updatedContact)
    {
      var contact = _context.Contacts.First(c => c.Id == id);
      contact.Name = updatedContact.Name;
      contact.PhoneNumber = updatedContact.PhoneNumber;
      contact.Email = updatedContact.Email;

      _context.SaveChangesAsync();

    }
  }
}
