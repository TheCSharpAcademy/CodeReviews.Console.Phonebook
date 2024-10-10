using PhoneBook.Data.Models;
using PhoneBook.Dtos;

namespace PhoneBook.Contracts
{
  public interface IContactService
	{
		void AddContact(ContactDto contact);

		void DeleteContact(int id);

		List<Contact> GetAllContacts();

		void UpdateContact(int id, ContactDto updatedContact);

		Contact GetContact(int id);
	}
}
