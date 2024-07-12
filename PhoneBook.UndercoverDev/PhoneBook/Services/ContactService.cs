
using PhoneBook.Controllers;
using PhoneBook.Models;
using PhoneBook.Utilities;
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
            var category = CategoryService.GetCategoriesOptionInput();

            if (category == null)
            {
                AnsiConsole.MarkupLine("[red]No categories available. Add a new Category first before deleting.[/]");
            }
            else
            {
                var contacts = ContactController.GetContactsByCategory(category.CategoryId);

                if (contacts == null || contacts.Count == 0)
                {
                    AnsiConsole.MarkupLine("[red]No contacts found in this category. Add a new Contact first before deleting.[/]");
                }
                else
                {
                    var contact = GetContactOptionInput();

                    if (contact == null)
                    {
                        AnsiConsole.MarkupLine("[red]No contacts available. Add a new Contact first before deleting.[/]");
                    }
                    else
                    {
                        ContactController.Delete(contact);
                        AnsiConsole.MarkupLine("[green]Deleted contact successfully[/]");
                    }
                }
            }
            
            MainMenu.ShowMainMenu();
        }

        private static Contact? GetContactOptionInput()
        {
            var contacts = ContactController.GetContacts();

            if (contacts == null || contacts.Count == 0)
            {
                return null;
            }

            var contactsArray = contacts.Select(c => $"{c.Name}: {c.PhoneNumber}").ToArray();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[bold][yellow]Select a contact to delete:[/][/]")
                    .AddChoices(contactsArray)
            );

            var phoneNumber = choice.Split(':').Last().Trim();
            
            return contacts.Single(c => c.PhoneNumber.Equals(phoneNumber));
        }

        internal static void UpdateContact()
        {
            AnsiConsole.MarkupLine("[bold]Select the Category where contact is to be updated[/]");
            var category = CategoryService.GetCategoriesOptionInput();
            if (category == null)
            {
                AnsiConsole.MarkupLine("[red]No categories available. Add a new Category first before updating.[/]");
            }
            else
            {
                var contacts = ContactController.GetContactsByCategory(category.CategoryId);

                if (contacts == null || contacts.Count == 0)
                {
                    AnsiConsole.MarkupLine("[red]No contacts found in this category. Add a new Contact first before updating.[/]");
                    return;
                }

                var contactsArray = contacts.Select(c => c.Name).ToArray();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\n[bold][yellow]Select a contact to update:[/][/]")
                        .AddChoices(contactsArray)
                );

                var selectedContact = contacts.Single(c => c.Name.Equals(choice));

                selectedContact.Name = AnsiConsole.Confirm("Update name?") ?
                AnsiConsole.Ask<string>("Enter new contact name: ") : selectedContact.Name;

                selectedContact.PhoneNumber = AnsiConsole.Confirm("Update phone number?") ?
                AnsiConsole.Ask<string>("Enter new contact phone number: ") : selectedContact.PhoneNumber;

                selectedContact.Email = AnsiConsole.Confirm("Update email?") ?
                AnsiConsole.Ask<string>("Enter new contact email: ") : selectedContact.Email;

                ContactController.Update(selectedContact);
                AnsiConsole.MarkupLine("[green]Updated contact successfully[/]");
            }
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
            if (ValidationHelper.ContactExists(new ContactContext(), contact.PhoneNumber))
            {
                AnsiConsole.MarkupLine("[red]Contact already exists.[/]");
            }
            else
            {
                contact.Category = category;
                ContactController.Add(contact);
                AnsiConsole.MarkupLine("[green]Added contact to category successfully[/]");
            }
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