using Microsoft.EntityFrameworkCore;
using Phonebook.wkktoria.Models;

namespace Phonebook.wkktoria.Services;

public class CategoryService
{
    private readonly AppDbContext _db = new();

    public List<Category> GetAllCategories()
    {
        try
        {
            var categories = _db.Categories
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