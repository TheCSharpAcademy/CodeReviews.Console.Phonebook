using Phone_Book.Lawang.Models;

namespace Phone_Book.Lawang.Controller;

public class CategoryController
{
    private PhoneBookContext _context;
    public CategoryController(PhoneBookContext context)
    {
        _context = context; 
    }

    public List<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }

    public Category CreateCategory(Category category)
    {
        var created =  _context.Categories.Add(category).Entity;
        _context.SaveChanges();

        return created;

    }
}
