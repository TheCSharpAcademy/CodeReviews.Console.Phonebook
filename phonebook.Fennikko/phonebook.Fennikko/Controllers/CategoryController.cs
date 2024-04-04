using Microsoft.EntityFrameworkCore;
using phonebook.Fennikko.Models;

namespace phonebook.Fennikko.Controllers;

public class CategoryController
{
    public static void AddCategory(Category category)
    {
        using var db = new ContactContext();
        db.Categories.Add(category);
        db.SaveChanges();
    }

    public static void DeleteCategory(Category category)
    {
        using var db = new ContactContext();
        db.Categories.Remove(category);
        db.SaveChanges();
    }

    public static void UpdateCategory(Category category)
    {
        using var db = new ContactContext();
        db.Categories.Add(category);
        db.SaveChanges();
    }

    public static List<Category> GetCategories()
    {
        using var db = new ContactContext();

        var categories = db.Categories
            .Include(c => c.Contacts)
            .ToList();

        return categories;
    }
}