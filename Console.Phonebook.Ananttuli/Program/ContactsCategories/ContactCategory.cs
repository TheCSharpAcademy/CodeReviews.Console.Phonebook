using Program.Categories;
using Program.Contacts;

namespace Program.ContactsCategories;

public class ContactCategory
{
    public int ContactCategoryId { get; set; }

    public int ContactId { get; set; }
    public int CategoryId { get; set; }

    public Contact Contact { get; set; }
    public Category Category { get; set; }
}