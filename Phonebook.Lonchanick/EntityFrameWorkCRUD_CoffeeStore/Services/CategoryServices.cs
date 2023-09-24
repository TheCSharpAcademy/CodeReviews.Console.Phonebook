using PlayingSpectre.UserInterfaces;
using PlayingSpectre.Controllers;

namespace PlayingSpectre.Services;

internal class CategoryServices
{
	internal static void AddCategory()
	{
		var category = CategoryInterface.AddCategory();
		CategoryController.AddCategory(category);
	}
	internal static void ShowCategories()
	{
		var categories = CategoryController.GetCategories();
		CategoryInterface.PrintAllCategories(categories);
	}

	internal static void DeleteCategory()
	{
		var categories = CategoryController.GetCategories();
		var category = CategoryInterface.CategoryPickableMenuOptions(categories);
		CategoryController.DeleteCateogry(category);
	}

	internal static void GetCategory()
	{
		var categories = CategoryController.GetCategories();
		var category = CategoryInterface.CategoryPickableMenuOptions(categories);
		Console.WriteLine("  Category: "+category.categoryName);
		CoffeeInterface.PrintCoffeeList(category.Coffees);
	}
}
