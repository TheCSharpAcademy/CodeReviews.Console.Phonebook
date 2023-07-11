namespace LONCHANICK_PhoneBookConsoleApp;

internal class ContactRepository
{
	internal static void Add(Contact contact)
	{
		var db = new ContactDbContext();
		db.Add(contact);
		db.SaveChanges();
	}
	internal static List<Contact> GetContacts() 
	{
		var db = new ContactDbContext();
		return db.Contacts.ToList();
	}

	internal static Contact GetContactById(int id)
	{
		var db = new ContactDbContext();
		var result = db.Contacts.First(x => id == x.Id);
		return result;
	}

	internal static void Remove(Contact contact)
	{
		var db = new ContactDbContext();
		db.Contacts.Remove(contact);
		db.SaveChanges();
	}

	internal static void Update(Contact contact)
	{
		var db = new ContactDbContext();
		db.Update(contact);
		db.SaveChanges();
	}
}
