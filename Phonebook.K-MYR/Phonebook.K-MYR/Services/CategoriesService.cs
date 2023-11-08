using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Phonebook.K_MYR.Models;
using Spectre.Console;

namespace Phonebook.K_MYR;

internal class CategoriesService
{
    internal void AddCategory()
    {
        try
        {
            using var db = new ContactsContext();
            string name;

            do
            {
                name = GetCategoryName();
            } while (db.Categories.Where(c => c.Name == name).Any());

            db.Add(new Category { Name = name });
            db.SaveChanges();
        }
        catch (SqlException ex)
        {
            Helpers.WriteMessageAndWait($"An Error Occured Calling The Database| {ex.Message} | Press Any Key To Return");
        }
    }

    internal void UpdateCategory()
    {
        try
        {
            using var db = new ContactsContext();
            var category = GetCategoryInput();

            if (category.CategoryId == 1)
            {
                Helpers.WriteMessageAndWait("Category 'General' Can't Be Updated");
                return;
            }

            category.Name = AnsiConsole.Ask<string>("Category Name:");
            db.Update(category);
            db.SaveChanges();
        }
        catch (SqlException ex)
        {
            Helpers.WriteMessageAndWait($"An Error Occured Calling The Database| {ex.Message} | Press Any Key To Return");
        }
    }

    internal void DeleteCategory()
    {
        try
        {
            using var db = new ContactsContext();
            var category = GetCategoryInput();

            if (category.CategoryId == 1)
            {
                Helpers.WriteMessageAndWait("Category 'General' Can't Be Deleted");
                return;
            }

            db.Remove(category);
            db.SaveChanges();
        }
        catch (SqlException ex)
        {
            Helpers.WriteMessageAndWait($"An Error Occured Calling The Database| {ex.Message} | Press Any Key To Return");
        }
    }

    internal IEnumerable<Category> GetAllCategories()
    {
        try
        {
            using var db = new ContactsContext();
            return db.Categories;
        }
        catch (SqlException ex)
        {
            Helpers.WriteMessageAndWait($"An Error Occured Calling The Database| {ex.Message} | Press Any Key To Return");
            return new List<Category>();
        }
    }

    internal CategoryDTO? GetCategory()
    {
        try
        {
            Console.Clear();

            var category = GetCategoryInput();

            return new CategoryDTO
            {
                Name = category.Name,
                Contacts = category.Contacts.Select(x => new ContactDTO
                {
                    FullName = x.Name,
                    EmailAdress = x.EmailAdress,
                    PhoneNumber = x.PhoneNumber,
                    CategoryName = category.Name
                }).ToList()
            };
        }
        catch (SqlException ex)
        {
            Helpers.WriteMessageAndWait($"An Error Occured Calling The Database| {ex.Message} | Press Any Key To Return");
            return null;
        }
    }

    private Category GetCategoryInput()
    {
        Console.Clear();

        using var db = new ContactsContext();

        var categories = db.Categories.Include(c => c.Contacts);
        var category = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                            .Title("Choose a Category:")
                                            .AddChoices(categories.Select(c => c.Name).ToList()));

        return categories.Single(x => x.Name == category);
    }

    private string GetCategoryName()
    {
        string contactName;
        do
        {
            contactName = AnsiConsole.Ask<string>("Category Name (max 50):").Trim();
        } while (contactName.Length > 50);

        return contactName;
    }
}
