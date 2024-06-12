using Phonebook.Entities;

namespace Phonebook.Repositories.Interfaces;

public interface IContactRepository
{
    IEnumerable<Contact> GetAllContacts();
    Contact GetContactById(int id);
    void AddContact(Contact contact);
    void UpdateContact(Contact contact);
    void DeleteContact(Contact contact);
}