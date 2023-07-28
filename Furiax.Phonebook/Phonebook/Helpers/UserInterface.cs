using Phonebook.Model;
using Phonebook.Services;
using Spectre.Console;
using static Phonebook.Helpers.Enums;

namespace Phonebook.Helpers;

internal class UserInterface
{
	internal static void MainMenu()
	{
		bool appAlive = true;
		while (appAlive)
		{
			Console.Clear();
			var option = AnsiConsole.Prompt(new SelectionPrompt<MenuOptions>()
				.Title("What do you want to do ?")
				.AddChoices(
					MenuOptions.AddContact,
					MenuOptions.ShowContact,
					MenuOptions.ShowAllContacts,
					MenuOptions.UpdateContact,
					MenuOptions.DeleteContact,
					MenuOptions.ManageCategories,
					MenuOptions.SendEmail,
					MenuOptions.SendSMS,
					MenuOptions.ExitApplication));

			switch (option)
			{
				case MenuOptions.AddContact:
					PhonebookService.InsertContact();
					break;
				case MenuOptions.ShowContact:
					PhonebookService.GetContact();
					break;
				case MenuOptions.ShowAllContacts:
					PhonebookService.GetContacts();
					break;
				case MenuOptions.UpdateContact:
					PhonebookService.UpdateContact();
					break;
				case MenuOptions.DeleteContact:
					PhonebookService.DeleteContact();
					break;
				case MenuOptions.ManageCategories:
					CategoriesMenu();
					break;
				case MenuOptions.SendEmail:
					PhonebookService.SendEmail();
					break;
				case MenuOptions.SendSMS:
					PhonebookService.SendSMS();
					break;
				case MenuOptions.ExitApplication:
					appAlive = false;
					break;
			}
		}
	}
	private static void CategoriesMenu()
	{
		bool IsCategoriesAlive = true;
		while (IsCategoriesAlive)
		{
			Console.Clear();
			var option = AnsiConsole.Prompt(new SelectionPrompt<CategoriesMenu>()
				.Title("Categories Menu")
				.AddChoices(
				Enums.CategoriesMenu.AddCategory,
				Enums.CategoriesMenu.UpdateCategory,
				Enums.CategoriesMenu.DeleteCategory,
				Enums.CategoriesMenu.ShowCategories,
				Enums.CategoriesMenu.Return));
			switch (option)
			{
				case Enums.CategoriesMenu.AddCategory:
					CategoryService.InsertCategory();
					break;
				case Enums.CategoriesMenu.UpdateCategory:
					CategoryService.UpdateCategory();
					break;
				case Enums.CategoriesMenu.DeleteCategory:
					CategoryService.DeleteCategory();
					break;
				case Enums.CategoriesMenu.ShowCategories:
					CategoryService.GetCategories();
					break;
				case Enums.CategoriesMenu.Return:
					IsCategoriesAlive = false;
					break;
			}
		}
	}
	internal static void DisplayContactTable(List<Contact> contacts)
	{
		var table = new Table();
		table.AddColumn("Id");
		table.AddColumn("Name");
		table.AddColumn("Phonenumber");
		table.AddColumn("Emailaddress");
		table.AddColumn("Category");

		foreach (var contact in contacts)
		{
			table.AddRow(contact.ContactId.ToString(),
				contact.Name,
				contact.PhoneNumber,
				contact.EmailAddress,
				contact.Category.Name);
		}

		AnsiConsole.Write(table);

		Console.WriteLine("Press any key to continue");
		Console.ReadLine();
		Console.Clear();
	}
	internal static void DisplayContactTable(Contact contact)
	{
		var panel = new Panel($@"Id: {contact.ContactId}
Name: {contact.Name}
Phonenumber: {contact.PhoneNumber}
Emailaddress= {contact.EmailAddress}
Category: {contact.Category.Name}");
		panel.Header = new PanelHeader("Contact info");
		panel.Padding = new Padding(2, 2, 2, 2);

		AnsiConsole.Write(panel);

		Console.WriteLine("Press any key to continue");
		Console.ReadLine();
		Console.Clear();
	}
	internal static void DisplayCategoryTable(List<Category> categories)
	{
		var table = new Table();
		table.AddColumn("Id");
		table.AddColumn("Name");

		foreach (var category in categories)
		{
			table.AddRow(category.CategoryId.ToString(), category.Name);
		}

		AnsiConsole.Write(table);

		Console.WriteLine("Press any key to continue");
		Console.ReadLine();
	}
	internal static void DisplayCategory(Category category)
	{
		var panel = new Panel($@"Id: {category.CategoryId}
Name: {category.Name}");
		panel.Header = new PanelHeader("Category info");
		panel.Padding = new Padding(2, 2, 2, 2);

		AnsiConsole.Write(panel);

		Console.WriteLine("Press any key to continue");
		Console.ReadLine();
		Console.Clear();
	}
}

