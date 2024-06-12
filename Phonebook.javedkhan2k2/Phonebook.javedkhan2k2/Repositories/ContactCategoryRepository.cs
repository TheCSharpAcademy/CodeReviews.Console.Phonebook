

using System.Security.Cryptography.X509Certificates;
using Phonebook.Data;
using Phonebook.Entities;
using Phonebook.Repositories.Interfaces;

namespace Phonebook.Repositories;

public class ContactCategoryRepository : IContactCategoryRepository
{
    private readonly PhonebookDbContext _context;

    public ContactCategoryRepository(PhonebookDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ContactCategory> GetAllContactCategories() => _context.ContactCategories.ToList();

    public void AddContactCategory(ContactCategory contactCategory)
    {
        _context.ContactCategories.Add(contactCategory);
        _context.SaveChanges();
    }

    public ContactCategory GetContactCategoryById(int id) => _context.ContactCategories.FirstOrDefault();


    public void UpdateContactCategory(ContactCategory contactCategory)
    {
        _context.ContactCategories.Update(contactCategory);
        _context.SaveChanges();
    }

    internal ContactCategory FindContactCategoryByName(string categoryName) => _context.ContactCategories.FirstOrDefault(c => c.CategoryName == categoryName);

}