using phonebook.Fennikko.Controllers;
using phonebook.Fennikko.Models;
using Spectre.Console;

namespace phonebook.Fennikko.Services;

public class CategoryService
{
    public static void InsertCategory()
    {
        var category = new Category();
        category.Name = AnsiConsole.Ask<string>("Category's name: ");

        CategoryController.AddCategory(category);
    }

    public static void DeleteCategory()
    {
        var category = GetCategoryOptionInput();
        CategoryController.DeleteCategory(category);
    }

    public static void UpdateCategory()
    {
        var category = GetCategoryOptionInput();
        category.Name = AnsiConsole.Confirm("Update name?")
            ? AnsiConsole.Ask<string>("New category name: ")
            : category.Name;

        CategoryController.UpdateCategory(category);
    }

    public static void GetCategories()
    {
        var categories = CategoryController.GetCategories();
        UserInterface.ShowCategoryTable(categories);
    }

    public static void GetCategory()
    {
        var category = GetCategoryOptionInput();
        UserInterface.ShowCategory(category);
    }
    public static Category GetCategoryOptionInput()
    {
        var categories = CategoryController.GetCategories();
        var categoriesArray = categories.Select(c => c.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose a category")
            .AddChoices(categoriesArray));
        var category = categories.Single(c => c.Name == option);

        return category;
    }
}