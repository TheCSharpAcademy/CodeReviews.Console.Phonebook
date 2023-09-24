using Microsoft.EntityFrameworkCore;
using PlayingSpectre.Models;

namespace PlayingSpectre.Controllers;

internal class CategoryController
{
	internal static int AddCategory(Category category)
	{
		using var dbRepository = new CoffeeDBcontext();
		dbRepository.Add(category);
		var status = dbRepository.SaveChanges();
		return status;
	}

	internal static List<Category> GetCategories()
	{
		using var db = new CoffeeDBcontext();
		var categories = db.Categories
			.Include(x => x.Coffees)
			.ToList();
		return categories;
	}

	internal static void DeleteCateogry(Category category)
	{
		using var db = new CoffeeDBcontext();
		db.Remove(category);
		db.SaveChanges();
	}
}
