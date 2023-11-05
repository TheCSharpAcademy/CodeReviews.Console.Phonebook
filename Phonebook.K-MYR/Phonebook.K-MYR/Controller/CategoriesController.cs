using Phonebook.K_MYR.Models;

namespace Phonebook.K_MYR;

internal class CategoriesController
{
    private readonly CategoriesService _categoriesService;

    public CategoriesController(CategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }
    
    internal void AddCategory()
    {
        _categoriesService.AddCategory();
    }

    internal void DeleteCategory()
    {
        _categoriesService.DeleteCategory();

    }

    internal void UpdateCategory()
    {
        _categoriesService.UpdateCategory();
    }

    internal IEnumerable<Category> GetAllCategories()
    {
        return _categoriesService.GetAllCategories();
    }

    internal Category GetCategory()
    {
        return _categoriesService.GetCategory();
    }
}
