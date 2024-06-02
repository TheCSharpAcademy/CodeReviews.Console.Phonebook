using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook.Queries;

internal class DbQueries
{
    internal static List<string> GetAllCategories()
    {
        using var context = new ContactContext();
        return context.Categories.Select(c => c.CategoryName).ToList();
    }

    internal static int GetCategoryId(string categoryName)
    {
        using var context = new ContactContext();
        var category = context.Categories.FirstOrDefault(c => c.CategoryName == categoryName);
        if (category != null)
            return category.Id;
        else
        {
            throw new Exception("Category not found");
        }
    }

    internal static void CreateDefaultCategory()
    {
        using ContactContext context = new ContactContext();
        var defaultCategory = context.Categories.FirstOrDefault(c => c.CategoryName == "Default");
        if (defaultCategory == null)
        {
            defaultCategory = new Category
            {
                CategoryName = "Default"
            };
            context.Categories.Add(defaultCategory);
            context.SaveChanges();
        }
    }

    internal static void CreateCategory(string categoryName)
    {
        using ContactContext context = new ContactContext();
        var newCategory = new Category
        {
            CategoryName = categoryName
        };
        context.Categories.Add(newCategory);
        context.SaveChanges();
    }

    internal static bool IsCategoryAlreadyPresent(string categoryName)
    {
        using ContactContext context = new ContactContext();
        var defaultCategory = context.Categories.FirstOrDefault(c => c.CategoryName == categoryName);
        if (defaultCategory != null)
        {
            Console.WriteLine("The following category name is already present select a new name");
            return true;
        }
        return false;
    }

    internal static List<Contact> GetAllContacts()
    {
        using ContactContext context = new ContactContext();
        var contacts = new List<Contact>();
        foreach (var contact in context.Contacts)
        {
            contacts.Add(contact);
        }
        return contacts;
    }

    internal static List<Contact> GetContactsSpecificCategory(string category)
    {
        using var context = new ContactContext() ;
        int categoryId = GetCategoryId(category);
        var contacts = context.Contacts
            .Where(b => b.CategoryId==categoryId)
            .ToList();
        return contacts;
    }
}
