using Phonebook.Entities;
namespace Phonebook.Repositories.Interfaces;

public interface IContactCategoryRepository
{
    IEnumerable<ContactCategory> GetAllContactCategories();
    ContactCategory GetContactCategoryById(int id);
    void AddContactCategory(ContactCategory contactCategory);
    void UpdateContactCategory(ContactCategory contactCategory);
}