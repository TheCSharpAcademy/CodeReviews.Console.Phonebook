using Phonebook.kwm0304.Models;

namespace Phonebook.kwm0304.Interfaces;

public interface IContactRepository
{
  Task<List<Contact>> GetAllContactsAsync();
  Task<List<Contact>> GetContactByName(string name);
  Task<List<Contact>> GetContactsByGroup(string group);
  Task AddContact(Contact contact);
  Task<bool> DeleteContact(Contact contact);
  Task<bool> UpdateContact(Contact contact);
}