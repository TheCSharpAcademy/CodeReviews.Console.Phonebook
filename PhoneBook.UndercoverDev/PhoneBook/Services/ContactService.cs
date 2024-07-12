
using PhoneBook.Controllers;
using PhoneBook.Models;
using PhoneBook.Views;
using Spectre.Console;

namespace PhoneBook.Services
{
    public class ContactService
    {
        internal static void AddContact()
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[bold][yellow]Do you want to use a previous category or create a new one?[/][/]")
                    .AddChoices(
                        "Use Previous Category",
                        "Add New Category"
                    )
            );

            if (choice.Equals("Use Previous Category"))
                UsePreviousCategory();
            else
                AddNewCategory();

            MainMenu.ShowMainMenu();
        }

        internal static void DeleteContact()
        {
            throw new NotImplementedException();
        }

        internal static void UpdateContact()
        {
            throw new NotImplementedException();
        }

        internal static void ViewAllContacts()
        {
            var contacts = ContactController.GetContacts();
            ContactView.DisplayContacts(contacts);
            MainMenu.ShowMainMenu();
        }

        internal static void UsePreviousCategory()
        {
            var selectedCategory = CategoryService.GetCategoriesOptionInput();
            AddContactToCategory(selectedCategory);
        }

        internal static void AddNewCategory()
        {
            var category = UserInteraction.UserInteractions.GetCategoryDetails();
            CategoryController.Add(category);
            AnsiConsole.MarkupLine("[green]Added category successfully[/]");
            AddContactToCategory(category);
        }

        internal static void AddContactToCategory(Category category)
        {
            var contact = UserInteraction.UserInteractions.GetContactDetails();
            contact.Category = category;
            ContactController.Add(contact);
            AnsiConsole.MarkupLine("[green]Added contact to category successfully[/]");
        }
    }
}