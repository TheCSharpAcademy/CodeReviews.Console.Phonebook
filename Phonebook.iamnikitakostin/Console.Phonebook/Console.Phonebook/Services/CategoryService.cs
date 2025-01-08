using Console.Phonebook.Controllers;
using Console.Phonebook.Data;
using Console.Phonebook.Models;

namespace Console.Phonebook.Services;
internal class CategoryService : ConsoleController
{
    private readonly DataContext _context;

    public CategoryService(DataContext context)
    {
        _context = context;
    }

    public bool Add(Category category)
    {
        try
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage($"There has been an error while executing the command: {ex.Message}");
            return false;
        }
    }
    public bool Delete(int id)
    {
        try
        {
            if (id == 1)
            {
                ErrorMessage("You cannot delete default General category.");
                return false;
            }

            var category = GetCategoryById(id);
            var contactsWithCategory = _context.Contacts.Where(c => c.Category.Id == id).ToList();

            if (contactsWithCategory.Count > 0)
            {
                Category generalCategory = GetCategoryById(1);
                foreach (Contact contact in contactsWithCategory)
                {
                    contact.Category = generalCategory;
                }
            }

                
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage($"An error occurred: {ex.Message}");
            return false;
        }
    }

    public Dictionary<int, string> GetCategories()
    {
        return _context.Categories.ToDictionary(c => c.Id, c => c.Name);
    }

    public Category? GetCategoryById(int id) {
        try
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);

            return category ?? throw new Exception("Category is not found.");
        }
        catch (Exception ex) {
            ErrorMessage($"There has been an error while executing the command: {ex.Message}");
            return null;
        }
    }
}
