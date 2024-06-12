

using Phonebook.Data;
using Phonebook.Entities;
using Phonebook.Repositories;
using Spectre.Console;


namespace Phonebook.Services;

public class ContactCategoryService
{
    private ContactCategoryRepository _contactCategoryRepository;

    public ContactCategoryService(PhonebookDbContext context)
    {
        _contactCategoryRepository = new ContactCategoryRepository(context);
    }

    public void AddContactCategory()
    {
        var contactCategory = new ContactCategory();
        contactCategory.CategoryName = UserInput.GetStringInput("Enter A Category Name: ");
        while(_contactCategoryRepository.FindContactCategoryByName(contactCategory.CategoryName) != null)
        {
            AnsiConsole.Markup($"The [maroon]{contactCategory.CategoryName}[/] Exists in Database.\n");
            contactCategory.CategoryName = UserInput.GetStringInput("Enter A Category Name: ");
        }
        _contactCategoryRepository.AddContactCategory(contactCategory);
        AnsiConsole.Markup($"Contact Category {contactCategory.CategoryName} Added [green]Successfully[/].\n");
        VisualizationEngine.DisplayContinueMessage();
    }

    public void UpdateContactCategory()
    {
        var contactcategories = _contactCategoryRepository.GetAllContactCategories();
        VisualizationEngine.DisplayContactCategoriess(contactcategories, "Contact Categories Table");
        var id = UserInput.GetIntInput();
        // Validation goes here
        var contactCategory = contactcategories.FirstOrDefault(x => x.Id == id);
        if(contactCategory == null)
        {
            AnsiConsole.Markup($"Contact Category with id {id} not found!");
            VisualizationEngine.DisplayContinueMessage();
            return;
        }
        
        contactCategory.CategoryName = UserInput.GetStringInput("Enter A Category Name: ");
        _contactCategoryRepository.UpdateContactCategory(contactCategory);
        AnsiConsole.Markup($"Contact {contactCategory.CategoryName} Updated [green]Successfully[/]\n");
        VisualizationEngine.DisplayContinueMessage();
    }

    public void ViewAllContactCategories()
    {
        var contactcategories = _contactCategoryRepository.GetAllContactCategories();
        VisualizationEngine.DisplayContactCategoriess(contactcategories, "Contact Categories Table");
        VisualizationEngine.DisplayContinueMessage();
    }

}