using PhoneBook.Data.Models;
using PhoneBook.Dtos;

namespace PhoneBook.Contracts
{
  public interface IContactService
	{
		Task AddContact(ContactDto contact);

		Task DeleteContact(int id);

		Task<List<ContactDto>> GetAllContacts();

		Task UpdateContact(int id, ContactDto updatedContact);

		Task<Contact> GetProduct(int id)
	}
}
