using Program.ContactsCategories;

namespace Program.Contacts;

public class Contact
{
    public int ContactId { get; set; }
    public string Name { get; set; } = "";
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public List<ContactCategory> ContactCategories { get; set; } = new();
}

