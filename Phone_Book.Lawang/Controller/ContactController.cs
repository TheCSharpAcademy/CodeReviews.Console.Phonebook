using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Phone_Book.Lawang.Models;
using Phone_Book.Lawang.View;
using Spectre.Console;

namespace Phone_Book.Lawang.Controller;

public class ContactController
{
    private PhoneBookContext _context { get; set; }
    public ContactController(PhoneBookContext context)
    {
        _context = context;
    }

    public void CreateContact(Contact? contact)
    {
        if (contact == null) return;

        var entity = _context.Contacts.Add(contact).Entity;

        _context.SaveChanges();

        if (entity != null)
        {
            Visual.ShowResult("green", 1);
        }
        else
        {
            Visual.ShowResult("red", 0);
        }
    }

    public IEnumerable<Contact> GetAllContacts()
    {
        return _context.Contacts;
    }

    public void UpdateContact(Contact contact)
    {
        _context.Contacts.Update(contact);
        _context.SaveChanges();

        Visual.ShowResult("green", 1);
    }

    public void DeleteContact(Contact contact)
    {
        _context.Contacts.Remove(contact);
        _context.SaveChanges();

        Visual.ShowResult("green", 1);
    }

}
