using PhoneBook.Dejmenek.Data.Repositories;
using PhoneBook.Dejmenek.Helpers;
using PhoneBook.Dejmenek.Models;
using PhoneBook.Dejmenek.Services;
using Spectre.Console;

namespace PhoneBook.Dejmenek.Controllers;

public class CategoriesController
{
    private readonly UserInteractionService _userInteractionService;
    private readonly CategoriesRepository _categoriesRepository;

    public CategoriesController(UserInteractionService userInteractionService, CategoriesRepository categoriesRepository)
    {
        _userInteractionService = userInteractionService;
        _categoriesRepository = categoriesRepository;
    }

    public void AddCategory()
    {
        string name = _userInteractionService.GetCategoryName();

        while (_categoriesRepository.CategoryExists(name))
        {
            AnsiConsole.MarkupLine($"There is already a category named {name}. Please try a different name.");
            name = _userInteractionService.GetCategoryName();
        }

        Category category = new Category
        {
            Name = name
        };

        _categoriesRepository.AddCategory(category);
    }

    public void UpdateCategory()
    {
        Category? categoryToUpdate = GetCategory();

        if (categoryToUpdate is null)
        {
            AnsiConsole.MarkupLine("There are currently no categories available. Please create some categories before updating a category.");
            return;
        }

        string name = _userInteractionService.GetCategoryName();

        while (_categoriesRepository.CategoryExists(name))
        {
            AnsiConsole.MarkupLine($"There is already a category named {name}. Please try a different name.");
            name = _userInteractionService.GetCategoryName();
        }

        categoryToUpdate.Name = name;

        _categoriesRepository.UpdateCategory(categoryToUpdate);
    }

    public Category? GetCategory()
    {
        List<Category> categories = _categoriesRepository.GetCategories();

        if (categories.Count == 0)
        {
            return null;
        }

        Category chosenCategory = _userInteractionService.GetCategory(categories);

        return chosenCategory;
    }

    public void DeleteCategory()
    {
        Category? categoryToDelete = GetCategory();

        if (categoryToDelete is null)
        {
            AnsiConsole.MarkupLine("There are currently no categories available. Please create some categories before deleting a category.");
            return;
        }

        _categoriesRepository.DeleteCategory(categoryToDelete.Id);
    }

    public List<CategoryDTO> GetCategories()
    {
        var categories = _categoriesRepository.GetCategories();

        if (categories.Count == 0)
        {
            return [];
        }

        return Mapper.ToCategoryDTOs(categories);
    }
}
