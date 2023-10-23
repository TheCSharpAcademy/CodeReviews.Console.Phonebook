using Phonebook.wkktoria.Models;
using Phonebook.wkktoria.Models.Dtos;
using Phonebook.wkktoria.Services;
using Phonebook.wkktoria.Views;
using Spectre.Console;

namespace Phonebook.wkktoria.Controllers;

public class CategoryController
{
    private readonly CategoryService _categoryService = new();
    private readonly ContactService _contactService = new();

    public void ViewContactsInCategory()
    {
        var category = GetCategoryOptionInput();
        var categoryDto = new CategoryDto
        {
            Name = category.Name,
            Contacts = category.Contacts
        };

        var categoryContacts = _contactService
            .GetAllContacts()
            .Where(c => c.CategoryId == category.Id)
            .Select(c => new ContactDto
            {
                Name = c.Name,
                Category = c.Category,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            })
            .ToList();

        if (categoryContacts.Any()) CategoryView.ShowContactsInCategory(categoryDto, categoryContacts);
    }

    public void ViewCategories()
    {
        var categories = _categoryService.GetAllCategories().Select(c => new CategoryDto
        {
            Name = c.Name,
            Contacts = c.Contacts
        }).ToList();

        if (categories.Any())
            CategoryView.ShowCategoriesTable(categories);
        else
            Console.Write("No categories found in database.");
    }

    private Category GetCategoryOptionInput()
    {
        var categories = _categoryService.GetAllCategories();
        var categoriesNames = categories.Select(c => c.Name).ToList();

        var selectedCategory = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose category")
            .AddChoices(categoriesNames));

        var category = categories.Single(c => c.Name == selectedCategory);

        return category;
    }
}