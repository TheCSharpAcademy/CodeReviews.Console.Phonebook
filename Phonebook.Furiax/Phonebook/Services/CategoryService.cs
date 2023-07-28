using Phonebook.Controllers;
using Phonebook.Helpers;
using Phonebook.Model;
using Spectre.Console;

namespace Phonebook.Services;

internal class CategoryService
{
	internal static Category GetCategoryOptionInput()
	{
		var categories = CategoryController.GetCategories();
		var categoriesArray = categories.Select(x => x.Name).ToArray();
		var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
			.Title("Choose category")
			.AddChoices(categoriesArray));
		var category = categories.Single(x => x.Name == option);
		return category;
	}

	internal static void InsertCategory()
	{
		var category = new Category();
		category.Name = AnsiConsole.Ask<string>("Category's name:");

		CategoryController.AddCategory(category);
	}

	internal static void DeleteCategory()
	{
		if (CategoryController.GetCategories().Count == 0)
		{
			Console.WriteLine("The category list is empty");
			Console.ReadKey();
		}
		else
		{
			var category = GetCategoryOptionInput();
			CategoryController.DeleteCategory(category);
		}
	}

	internal static void UpdateCategory()
	{
		if (CategoryController.GetCategories().Count == 0)
		{
			Console.WriteLine("The category list is empty");
			Console.ReadKey();
		}
		else
		{
			var category = GetCategoryOptionInput();
			category.Name = AnsiConsole.Confirm("Update name?") ?
				AnsiConsole.Ask<string>("Enter the new name:")
				: category.Name;
			CategoryController.UpdateCategory(category);
			Console.Clear();
		}
	}
	internal static void GetCategories()
	{
		var categories = CategoryController.GetCategories();
		UserInterface.DisplayCategoryTable(categories);
	}
	internal static void GetCategory()
	{
		if (CategoryController.GetCategories().Count == 0)
		{
			Console.WriteLine("The category list is empty");
			Console.ReadKey();
		}
		else
		{
			var category = GetCategoryOptionInput();
			UserInterface.DisplayCategory(category);
		}
	}
}
