
using Phonebook.frockett.DataLayer;

namespace Phonebook.frockett.Service_Layer;

public class PhonebookService
{
    private readonly PhoneBookRepository phoneBookRepository;

    public PhonebookService(PhoneBookRepository phoneBookRepository)
    {
        this.phoneBookRepository = phoneBookRepository;
    }

}
