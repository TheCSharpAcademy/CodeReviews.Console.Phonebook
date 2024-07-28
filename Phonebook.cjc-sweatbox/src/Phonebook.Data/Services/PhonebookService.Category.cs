using Microsoft.EntityFrameworkCore;
using Phonebook.Data.Entities;

namespace Phonebook.Data.Services;

/// <summary>
/// Partial class for Category entity specific database methods.
/// </summary>
public partial class PhonebookService
{
    #region Methods

    public Category GetCategory(int id)
    {
        return _context.Category.Include(x => x.Contacts).Single(x => x.Id == id);
    }

    public Category GetCategory(string name)
    {
        return _context.Category.Include(x => x.Contacts).Single(x => x.Name == name);
    }

    public IReadOnlyList<Category> GetCategories()
    {
        return _context.Category.Include(x => x.Contacts).ToList();
    }

    #endregion
}
