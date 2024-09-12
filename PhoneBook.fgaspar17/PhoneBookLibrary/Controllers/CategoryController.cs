namespace PhoneBookLibrary;

public static class CategoryController
{
    public static List<Category> GetCategories()
    {
        using var db = new PhoneBookContext();
        var categories = db.Categories.ToList();
        return categories;
    }

    public static Category? GetCategoryByName(string name)
    {
        using var db = new PhoneBookContext();
        var category = db.Categories.SingleOrDefault(c => c.Name == name);
        return category;
    }

    public static void InsertCategory(Category category)
    {
        using var db = new PhoneBookContext();
        db.Categories.Add(category);
        db.SaveChanges();
    }

    public static void UpdateCategory(Category category)
    {
        using var db = new PhoneBookContext();
        db.Categories.Update(category);
        db.SaveChanges();
    }

    public static void DeleteCategory(Category category)
    {
        using var db = new PhoneBookContext();
        db.Categories.Remove(category);
        db.SaveChanges();
    }
}