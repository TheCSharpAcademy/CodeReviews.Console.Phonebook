using Microsoft.EntityFrameworkCore;
using Phonebook.Model;

namespace Phonebook.Controllers;

internal class CategoryController
{
	internal static void AddCategory(Category category)
	{
		using var db = new PhonebookContext();
		db.Add(category);
		db.SaveChanges();
	}
	internal static List<Category> GetCategories()
	{
		using var db = new PhonebookContext();
		var categories = db.Category
			.Include(x => x.Contacts)
			.ToList();
		return categories;
	}
	internal static void DeleteCategory(Category category)
	{
		using var db = new PhonebookContext();
		db.Remove(category);
		db.SaveChanges();
	}
	internal static void UpdateCategory(Category category)
	{
		using var db = new PhonebookContext();
		db.Update(category);
		db.SaveChanges();
	}
}
