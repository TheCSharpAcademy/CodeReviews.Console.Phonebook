
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
            if (contacts == null || contacts.Count == 0)
                AnsiConsole.MarkupLine("[red]No contacts available.[/]");
            else
                ContactView.DisplayContacts(contacts);
            MainMenu.ShowMainMenu();
        }

        internal static void UsePreviousCategory()
        {
            var selectedCategory = CategoryService.GetCategoriesOptionInput();
            if (selectedCategory == null)
            {
                AnsiConsole.MarkupLine("[red]No Categories available. Add a new Category first before a contact can be added.[/]");
                AddContact();
            }
            else
            {
                AddContactToCategory(selectedCategory);
            }
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

        internal static void SearchContactsByCategory()
        {
            var selectedCategory = CategoryService.GetCategoriesOptionInput();
            if (selectedCategory == null)
            {
                AnsiConsole.MarkupLine("[red]No Categories available. Add a new Category before a contact can be searched.[/]");
            }
            else
            {
                var contacts = ContactController.GetContactsByCategory(selectedCategory.CategoryId);
                if (contacts == null || contacts.Count == 0)
                    AnsiConsole.MarkupLine("[red]No contacts found in this category.[/]");
                else
                    ContactView.DisplayContacts(contacts);
            }
            MainMenu.ShowMainMenu();
        }
    }
}