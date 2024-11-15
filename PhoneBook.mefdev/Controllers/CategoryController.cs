using PhoneBook.mefdev.Service;
using PhoneBook.mefdev.Shared.Interfaces;
using Spectre.Console;

namespace PhoneBook.mefdev.Controllers;

internal class CategoryController : BaseController, IBaseController
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public void AddItem()
    {
        RenderCustomLine("DodgerBlue1", "ADD A CATEGORY");

        var categoryName = GetName();
        _categoryService.CreateCategory(categoryName);
        DisplayMessage("creating a category...", "green");
    }

    public void DeleteItem()
    {
        RenderCustomLine("DodgerBlue1", "DELETE A CATEGORY");

        var categories = _categoryService.GetAllCategories();
        if (categories == null)
        {
            DisplayMessage("Categories are not found or Empty", "red");
            return;
        }
        var categoryName = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Select a [red]category[/] to delete")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
            .AddChoices(categories.Select(c => c.Name)));
        var category = _categoryService.GetCategoryByName(categoryName);
        if (category == null)
        {
            DisplayMessage("A category is not found", "red");
            return;
        }
        if (ConfirmDeletion(category.Name))
        {
            _categoryService.DeleteCategory(category.Id);
            DisplayMessage("deleting a category...", "green");
        }
    }

    public void UpdateItem()
    {
        RenderCustomLine("DodgerBlue1", "UPDATE A CATEGORY");

        var categories = _categoryService.GetAllCategories();
        if(categories == null)
        {
            DisplayMessage("Categories are not found or Empty", "red");
            return;
        }
        var categoryName = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
           .Title("Select a [red]category[/] to update")
           .PageSize(10)
           .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
           .AddChoices(categories.Select(c => c.Name)));
        var category = _categoryService.GetCategoryByName(categoryName);
        if (category == null)
        {
            DisplayMessage("A category is not found", "red");
            return;
        }
        string name = GetName(category.Name);
        _categoryService.UpdateCategory(category.Id, name);
        DisplayMessage("updating a category...", "green");
        
    }

    public void ViewItem()
    {
        RenderCustomLine("DodgerBlue1", "Category");

        var categories = _categoryService.GetAllCategories();
        if (categories == null)
        {
            DisplayMessage("Categories are not found or Empty", "red");
            return;
        }
        var categoryName = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Select a [green]Contact[/] to view")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
            .AddChoices(categories.Select(c => c.Name)));
        var category = _categoryService.GetCategoryByName(categoryName);
        if (category == null)
        {
            DisplayMessage("A category is not found", "red");
            return;
        }
        DisplayItemTable(category);
    }

    public void ViewItems()
    {
        RenderCustomLine("DodgerBlue1", "CATEGORIES");

        var categories = _categoryService.GetAllCategories();
        if (categories == null)
        {
            DisplayMessage("Categories are not found or Empty", "red");
            return;
        }
        DisplayAllItems(categories);
    }
}