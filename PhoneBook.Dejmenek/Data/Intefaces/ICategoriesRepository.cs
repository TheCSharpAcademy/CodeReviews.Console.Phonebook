using PhoneBook.Dejmenek.Models;

namespace PhoneBook.Dejmenek.Data.Intefaces;

public interface ICategoriesRepository
{
    void AddCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(int id);
    bool CategoryExists(string name);
    List<Category> GetCategories();
}
