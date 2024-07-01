using Program.ContactsCategories;

namespace Program.Categories;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = "";
    public List<ContactCategory> ContactCategories { get; set; } = [];

    public override string ToString()
    {
        return Name;
    }
}