using Program.Categories;
using Program.Contacts;

namespace Program.ContactsCategories;

public class ContactCategory
{
    public int ContactCategoryId { get; set; }

    public int ContactId { get; set; }
    public int CategoryId { get; set; }

    private Contact? _contact;

    public Contact Contact
    {
        get => _contact ??
            throw new InvalidOperationException("Uninitialized property Contact");
        set => _contact = value;
    }

    private Category? _category;

    public Category Category
    {
        get => _category ??
            throw new InvalidOperationException("Uninitialized property Category");
        set => _category = value;
    }
}