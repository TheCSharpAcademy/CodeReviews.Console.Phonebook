using Microsoft.EntityFrameworkCore;
using Phonebook.kwm0304.Data;
using Phonebook.kwm0304.Interfaces;
using Phonebook.kwm0304.Models;
using Spectre.Console;

namespace Phonebook.kwm0304.Repositories;

public class ContactRepository : IContactRepository
{
  private readonly PhonebookContext _context;
  public ContactRepository(PhonebookContext context)
  {
    _context = context;
  }

  public async Task AddContact(Contact contact)
  {
    await _context.Contacts.AddAsync(contact);
    await _context.SaveChangesAsync();
  }

  public async Task<bool> DeleteContact(Contact contact)
  {
    try
    {
      _context.Contacts.Remove(contact);
      await _context.SaveChangesAsync();
      return true;
    }
    catch (Exception e)
    {
      AnsiConsole.WriteException(e);
      return false;
    }
  }

  public async Task<List<Contact>> GetAllContactsAsync()
  {
    return await _context.Contacts.OrderBy(c => c.ContactName).ToListAsync();
  }

  public async Task<List<Contact>> GetContactByName(string name)
  {
    return await _context.Contacts
        .Where(c => c.ContactName!.StartsWith(name))
        .ToListAsync();
  }

  public async Task<List<Contact>> GetContactsByGroup(string group)
  {
    return await _context.Contacts.Where(c => c.Group!.GroupName!.StartsWith(group)).ToListAsync();
  }

  public async Task<bool> UpdateContact(Contact contact)
  {
    _context.Contacts.Update(contact);
    try
    {
      await _context.SaveChangesAsync();
      return true;
    }
    catch (Exception e)
    {
      AnsiConsole.WriteException(e);
      return false;
    }
  }
}