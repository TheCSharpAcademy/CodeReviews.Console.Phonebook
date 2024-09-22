using Spectre.Console;

namespace PhoneBook;

public class ContactCategoryMenuHandler
{
    public void Display()
    {
        MenuPresentation.MenuDisplayer<GroupContactsByCategoriesMenuOptions>(() => "[blue]Group Contacts by Category Menu[/]", HandleMenuOptions);
    }

    private bool HandleMenuOptions(GroupContactsByCategoriesMenuOptions option)
    {
        switch (option)
        {
            case GroupContactsByCategoriesMenuOptions.Back:
                return false;
            case GroupContactsByCategoriesMenuOptions.AssignCategoryToContact:
                ContactCategoryService.AssigningContactCategory();
                break;
            case GroupContactsByCategoriesMenuOptions.RemoveCategoryFromContact:
                ContactCategoryService.DeleteContactCategory();
                break;
            case GroupContactsByCategoriesMenuOptions.ShowCategoriesForContact:
                ContactCategoryService.ShowCategoriesByContact();
                break;
            case GroupContactsByCategoriesMenuOptions.ShowContactsInCategory:
                ContactCategoryService.ShowContactsByCategory();
                break;
            default:
                AnsiConsole.WriteLine($"Unknow option: {option}");
                break;
        }

        return true;
    }
}