using Microsoft.EntityFrameworkCore;
using Phonebook.Data.Entities;

namespace Phonebook.Data.Services;

/// <summary>
/// Partial class for Contact entity specific database methods.
/// </summary>
public partial class PhonebookService
{
    #region Methods

    public bool AddContact(Contact contact)
    {
        _context.Contact.Add(contact);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public bool DeleteContact(Contact contact)
    {
        _context.Contact.Remove(contact);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public Contact GetContact(int id)
    {
        return _context.Contact.Include(x => x.Category).Single(x => x.Id == id);
    }

    public IReadOnlyList<Contact> GetContacts()
    {
        return _context.Contact.Include(x => x.Category).ToList();
    }

    public bool SetContact(Contact contact)
    {
        _context.Contact.Update(contact);
        var result = _context.SaveChanges();
        return result > 0;
    }

    #endregion
}
