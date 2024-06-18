public class ContactController
{
    private ContactRepository _contactRepository;

    public ContactController(ContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public void Add(Contact contact)
    {
        _contactRepository.Add(contact);
    }
}
