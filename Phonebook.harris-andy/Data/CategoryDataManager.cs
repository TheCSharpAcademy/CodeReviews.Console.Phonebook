using Microsoft.EntityFrameworkCore;

namespace Phonebook;

public class CategoryDataManager
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
        var categories = db.Categories
                .Include(c => c.Contacts)
                .ToList();
        return categories;
    }

    internal static void RemoveCategory(int categoryId)
    {
        using var db = new PhonebookContext();
        db.Remove(new Category { CategoryId = categoryId });
        db.SaveChanges();
    }

    internal static void UpdateCategory(Category category)
    {
        using var db = new PhonebookContext();
        db.Update(category);
        db.SaveChanges();
    }
}