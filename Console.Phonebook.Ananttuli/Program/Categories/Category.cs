using Program.ContactsCategories;

namespace Program.Categories;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public ICollection<ContactCategory> ContactCategories { get; set; }
}