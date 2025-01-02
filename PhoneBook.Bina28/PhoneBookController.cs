namespace PhoneBook.Bina28;

internal class PhoneBookController
{
	internal static void AddData(PhoneBook phoneBook)
	{

		using var db = new PhoneBookContext();
		db.Add(phoneBook);
		db.SaveChanges();
	}

	internal static void RemoveData(PhoneBook phoneBook)
	{
		using var db = new PhoneBookContext();
		db.Remove(phoneBook);
		db.SaveChanges();
	}

	internal static List<PhoneBook> ShowAllData()
	{
		using var db = new PhoneBookContext();
		var phonesData = db.PhoneBooks.ToList<PhoneBook>();
		return phonesData;
	}

	internal static void ShowData()
	{
		throw new NotImplementedException();
	}

	internal static void UpdateData(PhoneBook phoneBook)
	{
		using var db = new PhoneBookContext();
		db.Update(phoneBook);
		db.SaveChanges();
	}

	internal static PhoneBook GetPhoneDataById(int id)
	{
		using var db = new PhoneBookContext();
		var phoneData = db.PhoneBooks.SingleOrDefault(x => x.Id == id);
		return phoneData;
	}
}
