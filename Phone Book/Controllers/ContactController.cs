public class ContactController
{
    private ContactRepository _contactRepository;

    public ContactController(ContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public void Add(Contact contact)
    {
        if (contact.Name.Length > 0 && contact.PhoneNumbers.Count > 0)
        {
            _contactRepository.Add(contact);
        }

        throw new ArgumentNullException("Error, invalid record.");
    }

    public List<Contact> GetAll() => _contactRepository.GetAllContacts();
}
