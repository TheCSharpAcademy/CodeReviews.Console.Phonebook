using PhoneBook.Controllers;
using PhoneBook.Models;
using PhoneBook.Utilities;
using PhoneBook.Views;
using Spectre.Console;

namespace PhoneBook.Services
{
    public class CategoryService
    {
        public static void AddCategory()
        {
            var category = UserInteraction.UserInteractions.GetCategoryDetails();
            if (ValidationHelper.CategoryExists(new ContactContext(), category.Name))
            {
                AnsiConsole.MarkupLine("[red]Category already exists. Please choose a different name[/]");
                AddCategory();
            }
            else
            {
                Console.Clear();
                CategoryController.Add(category);
                AnsiConsole.MarkupLine("[green]Added category successfully[/]");
            }
            MainMenu.ShowMainMenu();
        }

        internal static void ViewCategories()
        {
            Console.Clear();
            var categories = CategoryController.GetCategories();
            if (categories == null || categories.Count == 0)
                AnsiConsole.MarkupLine("[red]No Category available[/]");
            else
                CategoryView.DisplayCategories(categories);
            MainMenu.ShowMainMenu();
        }

        internal static void EditCategories()
        {
            Console.Clear();
            var category = GetCategoriesOptionInput();

            if (category == null)
            {
                AnsiConsole.MarkupLine("[red]No Categories available. Add a new Category[/]");
            }
            else
            {
                var newName = AnsiConsole.Prompt(
                    new TextPrompt<string>("\n[bold]Enter new category name[/]: ")
                        .Validate(name =>
                        {
                            return !string.IsNullOrWhiteSpace(name)
                                ? ValidationResult.Success()
                                : ValidationResult.Error("[red]Category name cannot be empty![/]");
                        }));

                category.Name = newName;
                CategoryController.Update(category);
                AnsiConsole.MarkupLine("[green]Updated category successfully[/]");
            }
            MainMenu.ShowMainMenu();
        }


        internal static void ViewContactsInCategories()
        {
            var category = GetCategoriesOptionInput();
            CategoryView.DisplayContacts(category);
            MainMenu.ShowMainMenu();
        }

        internal static void DeleteCategory()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold][red]Select a category to delete[/][/]");
            var category = GetCategoriesOptionInput();
            if (category == null)
            {
                AnsiConsole.MarkupLine("[red]No Categories available.[/]");
            }
            else
            {
                CategoryController.Delete(category);
                AnsiConsole.MarkupLine($"[green]Deleted [blue]{category.Name}[/] category successfully[/]");
            }
            MainMenu.ShowMainMenu();
        }

        internal static Category? GetCategoriesOptionInput()
        {
            var categories = CategoryController.GetCategories();

            if (categories == null || categories.Count == 0)
            {
                return null;
            }

            var categoriesArray = categories.Select(c => c.Name).ToArray();
            var option = AnsiConsole.Prompt(new
                SelectionPrompt<string>()
                .Title("\n[bold][blue]Select a Category[/][/]")
                .AddChoices(categoriesArray)
            );

            return categories.Single(c => c.Name.Equals(option));
        }
    }
}