using PhoneBook.mefdev.Models;

namespace PhoneBook.mefdev.Service;

internal class CategoryService: PhoneBookService
{
	public CategoryService()
	{

	}

    public Category CreateCategory(string name)
    {
        var category = new Category
        {
            Name = name,
        };

        _db.Categories.Add(category);
        _db.SaveChanges();
        return category;
    }

    public List<Category> GetAllCategories()
    {
        var categories = _db.Categories.ToList();
        if (categories == null || categories.Count <= 0)
        {
            return null;
        }
        return categories;
    }


    public Category? GetCategoryByName(string name)
    {
        return _db.Categories
                             .FirstOrDefault(c => c.Name == name);
    }

    public bool UpdateCategory(int id, string newName)
    {
        var category = _db.Categories.Find(id);
        if (category == null)
        {
            return false;
        }
        category.Name = newName;
        _db.SaveChanges();
        return true;
    }

    public bool DeleteCategory(int id)
    {
        var category = _db.Categories.Find(id);
        if (category == null)
        {
            return false;
        }
        _db.Categories.Remove(category);
        _db.SaveChangesAsync();
        return true;
    }
}