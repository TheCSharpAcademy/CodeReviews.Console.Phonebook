public class ContactRepository
{
    public void Add(Contact contact)
    {
        using (var _dbContext = new PhoneBookContext())
        {
            _dbContext.Add(contact);
            _dbContext.SaveChanges();
        }

    }

    public void Update(Contact contact)
    {
    }

    public void Remove(Contact contact)
    {
    }

    public Contact GetContact(Contact contact)
    {
        throw new NotImplementedException();
    }

    public List<Contact> GetAllContacts()
    {
        throw new NotImplementedException();
    }
}
