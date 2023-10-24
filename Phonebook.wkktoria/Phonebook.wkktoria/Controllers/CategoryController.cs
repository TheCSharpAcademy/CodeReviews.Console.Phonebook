using Microsoft.EntityFrameworkCore;
using Phonebook.wkktoria.Models;

namespace Phonebook.wkktoria.Controllers;

public class CategoryController
{
    private readonly AppDbContext _db = new();

    public void AddCategory(Category category)
    {
        try
        {
            _db.Add(category);
            _db.SaveChanges();
        }
        catch (Exception)
        {
            Outputs.ExceptionMessage("Failed to add category to database.");
        }
    }

    public void UpdateCategory(Category category)
    {
        try
        {
            _db.Update(category);
            _db.SaveChanges();
        }
        catch (Exception)
        {
            Outputs.ExceptionMessage("Failed to update category.");
        }
    }

    public void RemoveCategory(Category category)
    {
        try
        {
            _db.Remove(category);
            _db.SaveChanges();
        }
        catch (Exception)
        {
            Outputs.ExceptionMessage("Failed to remove category from database.");
        }
    }

    public List<Category> GetAllCategories()
    {
        try
        {
            var categories = _db.Categories!
                .Include(c => c.Contacts)
                .ToList();

            return categories;
        }
        catch (Exception)
        {
            Outputs.ExceptionMessage("Failed to get categories from database.");
        }

        return new List<Category>();
    }
}