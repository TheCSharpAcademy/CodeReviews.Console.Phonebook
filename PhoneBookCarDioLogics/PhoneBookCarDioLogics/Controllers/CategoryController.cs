using PhoneBookCarDioLogics.Models;
using Spectre.Console;

namespace PhoneBookCarDioLogics.Controllers;

internal class CategoryController
{
    internal static void InsertCategory()
    {
        var category = new Category();
        category.CategoryName = AnsiConsole.Ask<string>("What's the name of the new category?");

        using var context = new PhonebookAppDbContext();
        context.Add(category);
        context.SaveChanges();
    }

    internal static void DeleteCategory()
    {
        var context = new PhonebookAppDbContext();
        var category = GetCategoryOptionInput();

        if(category != null)
        {
            context.Remove(category);
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("No categories exist!");
            Console.ReadLine();
        }
    }

    internal static void UpdateCategory()
    {
        var category = GetCategoryOptionInput();
        if(category != null) 
        {
            category.CategoryName = AnsiConsole.Confirm("Update name?") ? AnsiConsole.Ask<string>("Contact's new name:") : category.CategoryName;

            var context = new PhonebookAppDbContext();
            context.Update(category);
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("No categories exist!");
            Console.ReadLine();
        }
    }

    internal static List<Category> GetCategories()
    {
        using var context = new PhonebookAppDbContext();

        var categories = context.Categories.ToList();

        UserInterface.ShowCategories(categories);

        return categories;
    }

    internal static Category GetCategoryOptionInput()
    {
        List<Category> categories = GetCategories();

        if(categories.Count > 0)
        {
            var categoriesArray = categories.Select(x => x.CategoryName).ToArray();
            var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Choose category")
                .AddChoices(categoriesArray));
            var category = categories.Single(x => x.CategoryName == option);

            return category;
        }
        else
        {
            return null;
        }
    }
}
