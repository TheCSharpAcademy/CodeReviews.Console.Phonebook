using Phonebook.Models;

namespace Phonebook.Controllers;

internal class EmailController
{
    internal static Email AddEmail(Email email)
    {
        using var db = new PhonebookContext();
        db.Add(email);
        db.SaveChanges();

        return email;
    }

    internal static void RemoveEmail(Email email)
    {
        using var db = new PhonebookContext();
        db.Remove(email);
        db.SaveChanges();
    }

    internal static List<Email> GetEmailsByContactID(Contact contact)
    {
        using var db = new PhonebookContext();
        List<Email> emails = db.Emails.Where(x => x.ContactId == contact.ContactId).ToList();
        return emails;
    }
}
