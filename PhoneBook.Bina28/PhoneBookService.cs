using Spectre.Console;

namespace PhoneBook.Bina28;
internal class PhoneBookService
{
	internal static void AddInput()
	{
		var phoneBook = new PhoneBook();
		phoneBook.Name = AnsiConsole.Ask<string>("Enter Name: ");
		phoneBook.PhoneNumber = Validation.GetValidPhoneNumber();
		phoneBook.Email = Validation.GetValidEmail();
		PhoneBookController.Add(phoneBook);
		AnsiConsole.MarkupLine("[green]Contact added successfully![/]");
		AwaitKeyPress();
	}

	internal static void DeleteInput()
	{
		var contact = GetPhoneBookOptionInput();
		if (contact == null)
		{
			Console.WriteLine( "The contact to be removed does not exist.");
			AwaitKeyPress();
			return;
		}
		PhoneBookController.Remove(contact);
		AnsiConsole.MarkupLine("[green]Contact removed successfully![/]");
		AwaitKeyPress();
	}
	static private PhoneBook? GetPhoneBookOptionInput()
	{
		var phoneBookData = PhoneBookController.Get();
		if (!phoneBookData.Any())
		{
			
			return null;
		}
		var phoneBookArray= phoneBookData.Select(x => x.Name).ToArray();
		var phoneBookOption = AnsiConsole.Prompt(
			new SelectionPrompt<string>()
				.Title("Select a contact: ")				
				.AddChoices(phoneBookArray)
		);
		var id=phoneBookData.First(x => x.Name == phoneBookOption).Id;
		var phoneBook = PhoneBookController.GetById(id);
		return phoneBook;
	}

	internal static void GetContacts()
	{
		Console.Clear();
		var phoneBookData = PhoneBookController.Get();
		UserInterface.ShowPhonebookTable(phoneBookData);
	}

	internal static void GetContactInput()
	{
		var phoneBookSingleData = GetPhoneBookOptionInput();
		if (phoneBookSingleData==null)
		{
			Console.WriteLine("The contact does not exist.");
			AwaitKeyPress();
			return;
		}
		UserInterface.ShowContact(phoneBookSingleData);
	}

	internal static void UpdateInput()
	{
		var contact = GetPhoneBookOptionInput();
		if (contact == null)
		{
			Console.WriteLine("The contact to be update does not exist.");
			AwaitKeyPress();
			return;
		}
		UserInterface.UpdateInput(contact);
		AwaitKeyPress();
	}

	static internal void AwaitKeyPress()
	{
		Console.WriteLine("\nPress any key to return to the menu...");
		Console.ReadKey();
	}
}
