using Phonebook.Entities;
using Spectre.Console;

namespace Phonebook.UI;

public class Menu
{
    public static string CancelOperation = $"[maroon]Go Back[/]";

    public string[] MainMenu = ["Phonebook", "Categories", "Exit"];
    public string[] PhonebookMenu = ["View All Contacts", "Send Email", "Send SMS", "Add Contact", "Update Contact", "Delete Contact", $"[maroon]Go Back[/]"];
    public string[] CategoryMenu = ["View All Categories", "Add Category", "Update Category", $"[maroon]Go Back[/]"];
    
    internal string GetMainMenu()
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please Select An Action From The Options Below")
                    .PageSize(10)
                    .AddChoices(MainMenu)
        );
    }

    internal string GetPhonebookMenu()
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please Select An Action From The Options Below")
                    .PageSize(10)
                    .AddChoices(PhonebookMenu)
        );
    }

    internal string GetCategoryMenu()
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please Select An Action From The Options Below")
                    .PageSize(10)
                    .AddChoices(CategoryMenu)
        );
    }

    internal static string GetContactCategoryMenu(IEnumerable<ContactCategory> contactCategories)
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please Select An Action From The Options Below")
                    .PageSize(10)
                    .AddChoices(Helpers.ConvertToArray(contactCategories, contactCategory => contactCategory.CategoryName).Append(CancelOperation))
        );
    }

}