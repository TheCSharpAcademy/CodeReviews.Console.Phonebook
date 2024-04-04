using phonebook.Fennikko.Controllers;
using phonebook.Fennikko.Models;
using Spectre.Console;

namespace phonebook.Fennikko.Services;

public class CategoryService
{
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