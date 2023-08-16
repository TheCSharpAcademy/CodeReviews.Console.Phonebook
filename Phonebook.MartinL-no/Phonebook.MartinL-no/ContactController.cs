﻿namespace Phonebook.MartinL_no;

internal class ContactController
{
    public static void AddContact(string name, string phoneNumber)
    {
        using var db = new ContactsContext();
        db.Add(new Contact { Name = name, PhoneNumber = phoneNumber });

        db.SaveChanges();
    }

    public static List<Contact> GetContacts()
    {
        using var db = new ContactsContext();

        var contacts = db.Contacts.ToList();

        return contacts;
    }

    public static Contact GetContactById(int id)
    {
        using var db = new ContactsContext();

        var contact = db.Contacts.FirstOrDefault(c => c.Id == id);

        return contact;
    }
}
