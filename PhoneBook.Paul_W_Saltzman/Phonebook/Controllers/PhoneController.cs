using Phonebook.Models;

namespace Phonebook.Controllers;

internal class PhoneController
{
    internal static Phone AddPhone(Phone newPhone)
    {
        using var db = new PhonebookContext();
        db.Add(newPhone);
        db.SaveChanges();
        return newPhone;
    }

    internal static void DeletePhone(Phone phoneToDelete)
    {
        using var db = new PhonebookContext();
        db.Remove(phoneToDelete);
        db.SaveChanges();
    }
}
