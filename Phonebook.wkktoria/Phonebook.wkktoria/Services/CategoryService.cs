using Phonebook.wkktoria.Controllers;
using Phonebook.wkktoria.Models;
using Phonebook.wkktoria.Models.Dtos;
using Phonebook.wkktoria.Views;
using Spectre.Console;

namespace Phonebook.wkktoria.Services;

public class CategoryService
{
    private readonly CategoryController _categoryController = new();
    private readonly ContactController _contactController = new();

    public void AddCategory()
    {
        var category = new Category
        {
            Name = AnsiConsole.Ask<string>("Name:")
        };

        var categories = _categoryController.GetAllCategories();

        if (categories.Any(c => string.Equals(c.Name, category.Name, StringComparison.InvariantCultureIgnoreCase)))
            Outputs.InvalidInputMessage("Category already exists.");
        else
            _categoryController.AddCategory(category);
    }

    public void UpdateCategory()
    {
        var category = GetCategoryOptionInput();

        category.Name = AnsiConsole.Ask<string>("Name:");

        _categoryController.UpdateCategory(category);
    }

    public void DeleteCategory()
    {
        var category = GetCategoryOptionInput();

        _categoryController.RemoveCategory(category);
    }

    public void ViewContactsInCategory()
    {
        var category = GetCategoryOptionInput();
        var categoryDto = new CategoryDto
        {
            Name = category.Name,
            Contacts = category.Contacts
        };

        var categoryContacts = _contactController
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
        else
            Console.WriteLine("No contacts found in selected category.");
    }

    public void ViewCategories()
    {
        var categories = _categoryController.GetAllCategories().Select(c => new CategoryDto
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
        var categories = _categoryController.GetAllCategories();
        var categoriesNames = categories.Select(c => c.Name).ToList();

        var selectedCategory = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose category")
            .AddChoices(categoriesNames));

        var category = categories.Single(c => c.Name == selectedCategory);

        return category;
    }
}