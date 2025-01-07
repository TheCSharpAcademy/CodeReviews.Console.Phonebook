namespace PhoneBook.Bina28;

internal class PhoneBookController
{
	internal static void Add(PhoneBook phoneBook)
	{
		using var db = new PhoneBookContext();
		db.Add(phoneBook);
		db.SaveChanges();
	}

	internal static void Remove(PhoneBook phoneBook)
	{		
		using var db = new PhoneBookContext();
		db.Remove(phoneBook);
		db.SaveChanges();			
	}

	internal static List<PhoneBook> Get()
	{
		using var db = new PhoneBookContext();
		var phonesData = db.PhoneBooks.ToList<PhoneBook>();
		return phonesData;
	}

	internal static void Update(PhoneBook phoneBook)
	{
		using var db = new PhoneBookContext();
		db.Update(phoneBook);
		db.SaveChanges();
	}

	internal static PhoneBook? GetById(int id)
	{
		using var db = new PhoneBookContext();
		var phoneData = db.PhoneBooks.SingleOrDefault(x => x.Id == id);
		return phoneData;
	}
}
