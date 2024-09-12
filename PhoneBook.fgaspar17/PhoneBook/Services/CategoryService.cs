using PhoneBookLibrary;
using Spectre.Console;

namespace PhoneBook;

public static class CategoryService
{
    public static void CreateCategory()
    {
        MenuPresentation.PresentMenu("[blue]Inserting[/]");
        bool isCancelled;
        string name;

        UniquePropertyValidator<string, Category> uniqueCategory = new()
        {
            ErrorMsg = "Name must be unique.",
            GetModel = CategoryController.GetCategoryByName
        };

        (isCancelled, name) = AskForCategoryName(uniqueCategory);
        if (isCancelled) return;

        CategoryController.InsertCategory(new Category { Name = name });
    }

    public static void UpdateCategory()
    {
        MenuPresentation.PresentMenu("[yellow]Updating[/]");
        bool isCancelled;
        string oldName, newName;

        ShowCategoryTable();

        AnsiConsole.WriteLine("Current Name");
        ExistingModelValidator<string, Category> existingCategory = new()
        {
            ErrorMsg = "Category Name doesn't exist.",
            GetModel = CategoryController.GetCategoryByName
        };

        (isCancelled, oldName) = AskForCategoryName(existingCategory);
        if (isCancelled) return;

        AnsiConsole.WriteLine("New Name");
        UniquePropertyValidator<string, Category> uniqueCategory = new()
        {
            ErrorMsg = "Name must be unique.",
            GetModel = CategoryController.GetCategoryByName,
            PropertyName = "Name",
            ExcludedValues = [(object)oldName]
        };
        (isCancelled, newName) = AskForCategoryName(uniqueCategory);
        if (isCancelled) return;

        CategoryController.UpdateCategory(new Category { Id = CategoryController.GetCategoryByName(oldName).Id, Name = newName });
    }

    public static void DeleteCategory()
    {
        MenuPresentation.PresentMenu("[red]Deleting[/]");
        bool isCancelled;
        string name;

        ShowCategoryTable();

        ExistingModelValidator<string, Category> existingCategory = new()
        {
            ErrorMsg = "Category Name doesn't exist.",
            GetModel = CategoryController.GetCategoryByName
        };

        (isCancelled, name) = AskForCategoryName(existingCategory);
        if (isCancelled) return;

        CategoryController.DeleteCategory(new Category { Id = CategoryController.GetCategoryByName(name).Id });
    }

    public static void ShowCategories()
    {
        ShowCategoryTable();
        Prompter.PressKeyToContinuePrompt();
    }

    public static (bool IsCancelled, string Result) AskForCategoryName(params IValidator[] validators)
    {
        string message = "Enter a Category Name";
        return Prompter.PromptWithValidation(message, validations: validators);
    }

    public static void ShowCategoryTable()
    {
        List<CategoryDto> categories = CategoryController.GetCategories().Select(c => CategoryMapper.MapToDto(c)).ToList();
        OutputRenderer.ShowTable(categories, "Categories");
    }
}