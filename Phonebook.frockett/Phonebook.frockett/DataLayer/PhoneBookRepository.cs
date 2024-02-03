
using Phonebook.frockett.Models;

namespace Phonebook.frockett.DataLayer;

public class PhoneBookRepository
{
    private readonly PhoneBookContext _context;

    public PhoneBookRepository(PhoneBookContext context)
    {
        _context = context;
    }

    public void AddContact(Contact newContact)
    {
        
    }
}
