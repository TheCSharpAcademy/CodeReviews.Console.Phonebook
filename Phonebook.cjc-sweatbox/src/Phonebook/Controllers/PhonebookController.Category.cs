using Phonebook.Data.Entities;

namespace Phonebook.Controllers;

/// <summary>
/// Partial class for Category specific model-database methods.
/// </summary>
public partial class PhonebookController
{
    #region Methods

    public Category GetCategory(int id)
    {
        return _service.GetCategory(id);
    }

    public Category GetCategory(string name)
    {
        return _service.GetCategory(name);
    }

    public IReadOnlyList<Category> GetCategories()
    {
        return _service.GetCategories();
    }

    #endregion
}
