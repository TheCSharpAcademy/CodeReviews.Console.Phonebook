using Phonebook.StanimalTheMan.Models;

namespace Phonebook.StanimalTheMan.Controllers;

internal class ContactController
{
    internal static void AddContact(Contact contact)
    {
        using var db = new ContactsContext();
        db.Add(contact);
        db.SaveChanges();
    }
}
