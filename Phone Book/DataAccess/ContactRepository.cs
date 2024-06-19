using Microsoft.EntityFrameworkCore;

public class ContactRepository : IDisposable
{
    private readonly PhoneBookContext _dbContext;

    public ContactRepository()
    {
        _dbContext = new PhoneBookContext();
    }

    public void Add(Contact contact)
    {
        _dbContext.Add(contact);
        _dbContext.SaveChanges();
    }

    public void Update(Contact contact)
    {
        _dbContext.Update(contact);
        _dbContext.SaveChanges();
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
        return _dbContext.Contacts
            .Include(e => e.EmailAddresses)
            .Include(p => p.PhoneNumbers)
            .ToList();
    }

    public void Dispose()
    {
        // This method disposes the DbContext instance used by this repository.
        // In the current implementation, we are not explicitly disposing the repository
        // or the DbContext. This is because if we use the "using" statement (dispose sugar syntax)
        // on each CRUD method, Entity Framework won't be able to track changes between instances,
        // unlike how we could with Dapper.
        //
        // To properly manage the lifetime of the DbContext and avoid issues,
        // we should use dependency injection and scoping in a larger application.
        // This will ensure that the DbContext is properly disposed when it is no longer needed.
        //
        // As an alternative, we could call this method explicitly when we are finished using the repository
        // in our entry point to ensure that the DbContext is disposed properly. However, this approach
        // leads to tighter coupling.
        _dbContext.Dispose();
    }
}
