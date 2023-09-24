using PlayingSpectre.Controllers;
using PlayingSpectre.Models;
using Spectre.Console;

namespace PlayingSpectre.UserInterfaces;

internal class CategoryInterface
{
    public static Category AddCategory()
    {
        var name = AnsiConsole.Ask<string>("Name: ");
        var aux = new Category(name);

        return aux;
    }

    public static void PrintAllCategories(List<Category> categories)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");

        foreach (var category in categories)
        {
            table.AddRow
                (category.categoryId.ToString(),
                category.categoryName);
        }

        AnsiConsole.Write(table);
        Console.Write("Press any Key to continue");
        Console.ReadLine();
    }

    public static Category CategoryPickableMenuOptions(List<Category> categories)
    {
        var option = AnsiConsole.Prompt(new SelectionPrompt<Category>()
            .Title("Choose any Category")
            .AddChoices(categories.ToList()));

        return option;
    }

    public static Category CategoryMenuPickable()
    {
        var categories = CategoryController.GetCategories();
        var category = CategoryPickableMenuOptions(categories);

        return category;
    }


}
