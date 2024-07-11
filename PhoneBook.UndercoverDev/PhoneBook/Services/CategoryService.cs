using PhoneBook.Controllers;
using PhoneBook.Models;
using PhoneBook.Views;
using Spectre.Console;

namespace PhoneBook.Services
{
    public class CategoryService
    {
        public static void AddCategory()
        {
            var category = UserInteraction.UserInteractions.GetCategoryDetails();
            CategoryController.Add(category);
        }

        internal static void ViewCategories()
        {
            var categories = CategoryController.GetCategories();
            CategoryView.DisplayCategories(categories);
            MainMenu.ShowMainMenu();
        }

        internal static void ViewContactsInCategories()
        {
            var category = GetCategoriesOptionInput();
            UserInteraction.UserInteractions.DisplayContacts(category);
            MainMenu.ShowMainMenu();
        }

        internal static Category GetCategoriesOptionInput()
        {
            var categories = CategoryController.GetCategories();
            var categoriesArray = categories.Select(c => c.Name).ToArray();
            var option = AnsiConsole.Prompt(new 
                SelectionPrompt<string>()
                .Title("\n[bold][blue]Select a Category to View Contacts[/][/]")
                .AddChoices(categoriesArray)
            );

            var id = categories.Single(c => c.Name == option).CategoryId;
            var category = CategoryController.GetCategoryById(id);

            return category;
        }
    }
}