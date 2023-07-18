using Phonebook.Model;

namespace Phonebook;

internal class PhonebookController
{
	internal static void AddContact(Contact contact)
	{
		using var db = new PhonebookContext();
		db.Add(contact);
		db.SaveChanges();	
	}
	internal static List<Contact> GetContacts()
	{
		using var db = new PhonebookContext();
		var contacts = db.Contacts.ToList();
		return contacts;
	}
}
