using Kmakai.PhoneBook.Data;
using Kmakai.PhoneBook.Models;
using Spectre.Console;

namespace Kmakai.PhoneBook.Controllers;

public class CategoryController
{
    public static List<Category> GetCategories()
    {
        using var db = new AppDbContext();
        return db.Categories.ToList();
    }   

    public static int GetCategoryIdByName()
    {

        var categories = GetCategories();
        var categoryName = AnsiConsole.Prompt<string>(new SelectionPrompt<string>()
                                        .Title("Select contact category")
                                        .PageSize(10)
                                        .MoreChoicesText("[grey](Move up and down to reveal more categories)[/]")
                                        .AddChoices(categories.Select(x => x.Name).ToArray()));

        var category = categories.SingleOrDefault(x => x.Name == categoryName);

        return category.Id;
    }
}
