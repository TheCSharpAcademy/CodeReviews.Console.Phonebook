namespace PhoneBook.UgniusFalze;

public class ContactsController
{
    private ContactsContext _context { get; set; }

    public ContactsController()
    {
        _context = new ContactsContext();
    }

    public List<Contact> GetContacts()
    {
        using (_context)
        {
            var contacts = _context.Contacts.OrderBy(c => c.ContactId).ToList();
            return contacts;
        }
    }
}