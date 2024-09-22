using Spectre.Console;

namespace PhoneBook;

public class CategoryMenuHandler
{
    public void Display()
    {
        MenuPresentation.MenuDisplayer<CategoryMenuOptions>(() => "[yellow]Category Menu[/]", HandleMenuOptions);
    }

    private bool HandleMenuOptions(CategoryMenuOptions option)
    {
        switch (option)
        {
            case CategoryMenuOptions.Back:
                return false;
            case CategoryMenuOptions.CreateCategory:
                CategoryService.CreateCategory();
                break;
            case CategoryMenuOptions.UpdateCategory:
                CategoryService.UpdateCategory();
                break;
            case CategoryMenuOptions.DeleteCategory:
                CategoryService.DeleteCategory();
                break;
            case CategoryMenuOptions.ShowCategories:
                CategoryService.ShowCategories();
                break;
            default:
                AnsiConsole.WriteLine($"Unknow option: {option}");
                break;
        }

        return true;
    }
}