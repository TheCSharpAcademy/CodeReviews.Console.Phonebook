using phonebook.Fennikko.Controllers;
using phonebook.Fennikko.Models;
using Spectre.Console;

namespace phonebook.Fennikko.Services;

public class CategoryService
{
    public static void InsertCategory()
    {
        var category = new Category
        {
            Name = AnsiConsole.Ask<string>("Category's name: ")
        };

        CategoryController.AddCategory(category);
    }

    public static void DeleteCategory()
    {
        var category = GetCategoryOptionInput();
        if (category == null)
        {
            AnsiConsole.Write("No categories available. Press any key to return to the Category Menu");
            Console.ReadKey();
            
            UserInterface.CategoryMenu();
        }
        CategoryController.DeleteCategory(category);
    }

    public static void UpdateCategory()
    {
        var category = GetCategoryOptionInput();
        if (category == null)
        {
            AnsiConsole.Write("No categories available. Press any key to return to the Category Menu");
            Console.ReadKey();
            
            UserInterface.CategoryMenu();
        }
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
        if (category == null)
        {
            AnsiConsole.Write("No categories available. Press any key to return to the Category Menu");
            Console.ReadKey();
            
            UserInterface.CategoryMenu();
        }
        UserInterface.ShowCategory(category);
    }

    public static Category? GetCategoryOptionInput()
    {
        var categories = CategoryController.GetCategories();
        var categoriesArray = categories.Select(c => c.Name).ToArray();
        if (categoriesArray.Length == 0)
            return null;

        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose a category")
            .AddChoices(categoriesArray));

        var category = categories.Single(c => c.Name == option);

        return category;
    }
}