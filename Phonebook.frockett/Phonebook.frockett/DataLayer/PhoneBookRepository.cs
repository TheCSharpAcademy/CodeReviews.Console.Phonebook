﻿using Microsoft.EntityFrameworkCore;
using Phonebook.frockett.Models;


namespace Phonebook.frockett.DataLayer;

public class PhoneBookRepository
{
    private readonly PhoneBookContext _context;

    public PhoneBookRepository(PhoneBookContext context)
    {
        _context = context;
    }

    #region "Contact Methods"
    public Contact AddContact(Contact contact)
    {
        var contactToAdd = new Contact
        {
            Name = contact.Name,
            PhoneNumber = contact.PhoneNumber,
            Email = contact.Email,
        };
        _context.Contacts.Add(contactToAdd);
        _context.SaveChanges();

       return contactToAdd; // return so I can have the ID if needed
    }

    public List<Contact> GetAllContacts()
    {
        List<Contact> contacts = _context.Contacts.Include(c => c.ContactGroup).OrderBy(c => c.Name).ToList(); 
        return contacts;
    }

    public Contact? GetContactById(int id)
    {
        var contact = _context.Contacts.Find(id);

        return contact;
    }

    public Contact? UpdateContactInfo(Contact contact)
    {
        var contactToUpdate = _context.Contacts.FirstOrDefault(c => c.ContactId == contact.ContactId);
        if (contact.PhoneNumber != null)
        {
            contactToUpdate.PhoneNumber = contact.PhoneNumber;
            _context.SaveChanges();
        }
        
        if (contact.Email != null)
        {
            contactToUpdate.Email = contact.Email;
            _context.SaveChanges();
        }
        
        if (contact.Name != null)
        {
            contactToUpdate.Name = contact.Name;
            _context.SaveChanges();
        }

        return contactToUpdate;
    }

    public bool RemoveContactById(Contact contact)
    {
        var contactToDelete = _context.Contacts.FirstOrDefault(c => c.ContactId == contact.ContactId);

        if (contactToDelete != null)
        {
            _context.Contacts.Remove(contactToDelete);
            _context.SaveChanges();
            return true;
        }
        else return false;
    }
    #endregion

    #region "ContactGroup Methods"
    public ContactGroup? GetGroupWithContacts(int groupId)
    {
        ContactGroup contactGroup = _context.ContactGroup
            .Include(g => g.Contacts)
            .FirstOrDefault(g => g.ContactGroupId == groupId);

        return contactGroup;
    }

    public ContactGroup? AddContactGroup(string groupName)
    {
        var newGroup = new ContactGroup
        {
            Name = groupName
        };

        _context.ContactGroup.Add(newGroup);
        _context.SaveChanges();

        return newGroup;
    }
    
    public bool AddContactToGroup(int contactId, int groupId)
    {
        var contact = _context.Contacts.Find(contactId);
        if (contact == null) return false;

        // check to make sure the contact isn't already part of another group
        if (contact.GroupId != null) return false;

        var group = _context.ContactGroup.Find(groupId);
        if (group == null) return false;

        contact.GroupId = group.ContactGroupId;

        _context.SaveChanges();
        return true;
    }

    public bool RemoveContactFromGroup(int contactId, int groupId)
    {
        var contact = _context.Contacts.Find(contactId);
        if (contact == null) return false;

        var group = _context.ContactGroup.Find(groupId);
        if (group == null) return false;

        contact.GroupId = null;

        _context.SaveChanges();
        return true;
    }

    public List<ContactGroup> GetAllGroups()
    {
        List<ContactGroup> groupList = _context.ContactGroup.Include(g => g.Contacts).OrderBy(g => g.Name).ToList();

        foreach (var group in groupList)
        {
            group.Contacts = group.Contacts.OrderBy(c => c.Name).ToList();
        }
        return groupList;
    }

    public ContactGroup? GetGroupByName(string name)
    {
        ContactGroup contactGroup = _context.ContactGroup.FirstOrDefault(g => g.Name == name);
        return contactGroup;
    }

    public bool UpdateContactGroupName(int groupId, string newName)
    {
        var contactGroup = _context.ContactGroup.Find(groupId);

        if (contactGroup == null)
        {
            return false;
        }
        contactGroup.Name = newName;
        _context.SaveChanges();

        return true;
    }

    public bool DeleteContactGroup(int groupId, bool deleteContacts)
    {
        var contactGroup = _context.ContactGroup
            .Include(g => g.Contacts) // Include contacts for potential deletion
            .FirstOrDefault(g => g.ContactGroupId == groupId);

        if (contactGroup == null)
        {
            return false;
        }

        if (deleteContacts)
        {
            _context.Contacts.RemoveRange(contactGroup.Contacts);
        }
        else
        {
            foreach (var contact in contactGroup.Contacts)
            {
                contact.GroupId = null; // Or set to a default group if applicable
            }
        }
        _context.ContactGroup.Remove(contactGroup);
        _context.SaveChanges();

        return true;
    }
    #endregion
}
