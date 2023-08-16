namespace Phonebook.MartinL_no;

internal class ContactController
{
    public static void AddContact(string name, string phoneNumber)
    {
        using var db = new ContactsContext();
        db.Add(new Contact { Name = name, PhoneNumber = phoneNumber });

        db.SaveChanges();
    }
}
