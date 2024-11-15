
using PhoneBook.mefdev.Context;

namespace PhoneBook.mefdev.Service;

internal class PhoneBookService
{
    public readonly PhonebookContext _db = new();

    protected PhoneBookService()
    {

    }
}
  