using Spectre.Console;

namespace PhoneBook.Bina28;
internal class PhoneBookService
{
	internal static async void AddData()
	{   var phoneBook = new PhoneBook();
	phoneBook.Name = AnsiConsole.Ask<string>("Enter Name: ");
		phoneBook.PhoneNumber = Validation.GetValidPhoneNumber();
		phoneBook.Email = Validation.GetValidEmail();
		PhoneBookController.AddData(phoneBook);
		AnsiConsole.MarkupLine("[green]Contact added successfully![/]");
		AwaitKeyPress();
	}

	internal static void DeleteContact()
	{
		var contact = GetPhoneBookOptionInput();
		PhoneBookController.RemoveData(contact);
		AnsiConsole.MarkupLine("[green]Contact removed successfully![/]");
		AwaitKeyPress();
	}
	static private PhoneBook GetPhoneBookOptionInput()
	{
		var phoneBookData = PhoneBookController.ShowAllData();
		var phoneBookArray= phoneBookData.Select(x => x.Name).ToArray();
		var phoneBookOption = AnsiConsole.Prompt(
			new SelectionPrompt<string>()
				.Title("Select a contact: ")				
				.AddChoices(phoneBookArray)
		);
		var id=phoneBookData.Single(x => x.Name == phoneBookOption).Id;
		var phoneBook = PhoneBookController.GetPhoneDataById(id);
		return phoneBook;
	}

	internal static void GetContacts()
	{
		Console.Clear();
		var phoneBookData = PhoneBookController.ShowAllData();
		UserInterface.ShowPhonebookTable(phoneBookData);
	}

	internal static void GetContact()
	{
		var phoneBookSingleData = GetPhoneBookOptionInput();
		UserInterface.ShowContact(phoneBookSingleData);
	}

	internal static void UpdateContact()
	{
		var contact = GetPhoneBookOptionInput();
	    contact.Name = AnsiConsole.Confirm("Update name?")
		? AnsiConsole.Ask<string>("Enter Name: ")
		: contact.Name;
		contact.PhoneNumber = AnsiConsole.Confirm("Update phone number?")
		? Validation.GetValidPhoneNumber()
		:contact.PhoneNumber;
		contact.Email = AnsiConsole.Confirm("Update Email?")
		? Validation.GetValidEmail()
		:contact.Email;
		PhoneBookController.UpdateData(contact);
		AnsiConsole.MarkupLine("[green]Contact updated successfully![/]");
		AwaitKeyPress();
	}

	static internal void AwaitKeyPress()
	{
		Console.WriteLine("\nPress any key to return to the menu...");
		Console.ReadKey();
	}
}
