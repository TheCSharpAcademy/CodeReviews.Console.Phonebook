
using Phonebook.frockett.DataLayer;
using Phonebook.frockett.DTOs;

namespace Phonebook.frockett.Service_Layer;

public class PhonebookService
{
    private readonly PhoneBookRepository phoneBookRepository;

    public PhonebookService(PhoneBookRepository phoneBookRepository)
    {
        this.phoneBookRepository = phoneBookRepository;
    }

    public void DeleteContact(ContactDTO contact)
    {
        throw new NotImplementedException();
    }
}
