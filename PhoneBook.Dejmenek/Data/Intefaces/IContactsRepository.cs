using PhoneBook.Dejmenek.Models;

namespace PhoneBook.Dejmenek.Data.Intefaces;

public interface IContactsRepository
{
    void AddContact(Contact contact);
    Contact GetContact(int id);
    void DeleteContact(int id);
    void UpdateContact(Contact contact);
    List<Contact> GetAllContacts();
    List<Contact> GetContactsByCategory(int categoryId);
    bool PhoneNumberExists(string phoneNumber);

}
