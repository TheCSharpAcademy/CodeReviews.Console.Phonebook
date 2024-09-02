using PhoneBook.Model;

namespace PhoneBook.Interfaces.Strategies;

internal interface IContactEditStrategy
{
    void Edit(Contact contact);
}