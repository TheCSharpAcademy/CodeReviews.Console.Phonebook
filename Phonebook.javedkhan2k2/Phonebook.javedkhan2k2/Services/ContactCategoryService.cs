

using Microsoft.Data.SqlClient;
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
        try
        {
            var contactCategory = UserInput.GetNewContactCategory();
            if (contactCategory == null)
            {
                AnsiConsole.Markup("You canceled the Operation\n");
                VisualizationEngine.DisplayContinueMessage();
                return;
            }
            _contactCategoryRepository.AddContactCategory(contactCategory);
            AnsiConsole.Markup($"Contact Category {contactCategory.CategoryName} Added [green]Successfully[/].\n");
            VisualizationEngine.DisplayContinueMessage();
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                AnsiConsole.Markup($"[maroon]{ex.InnerException.Message}[/]\n");
            }
            else
            {
                AnsiConsole.Markup($"[maroon]{ex.Message}[/]\n");
            }
            VisualizationEngine.DisplayContinueMessage();
        }
    }

    public void UpdateContactCategory()
    {
        try
        {
            var contactcategories = _contactCategoryRepository.GetAllContactCategories();
            var contactCategory = UserInput.UpdateContactCategory(contactcategories);
            if (contactCategory == null)
            {
                AnsiConsole.Markup("You canceled the Operation or Category not found\n");
                VisualizationEngine.DisplayContinueMessage();
                return;
            }
            _contactCategoryRepository.UpdateContactCategory(contactCategory);
            AnsiConsole.Markup($"Contact {contactCategory.CategoryName} Updated [green]Successfully[/]\n");
            VisualizationEngine.DisplayContinueMessage();
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                AnsiConsole.Markup($"[maroon]{ex.InnerException.Message}[/]\n");
            }
            else
            {
                AnsiConsole.Markup($"[maroon]{ex.Message}[/]\n");
            }
            VisualizationEngine.DisplayContinueMessage();
        }
    }

    public void ViewAllContactCategories()
    {
        var contactcategories = _contactCategoryRepository.GetAllContactCategories();
        VisualizationEngine.DisplayContactCategoriess(contactcategories, "Contact Categories Table");
        VisualizationEngine.DisplayContinueMessage();
    }

    public IEnumerable<ContactCategory> GetAllContactCategories() => _contactCategoryRepository.GetAllContactCategories();

}