using PhoneBook.Data.Models;

namespace PhoneBook.Contracts
{
  public interface IContactService
	{
		void AddContact(Contact contact);

		void DeleteContact(string email);

		List<Contact> GetAllContacts();

		void UpdateContact(string email, Contact updatedContact);

    Contact GetContact(string email);

		Contact GetValidatedContact();
	}
}
