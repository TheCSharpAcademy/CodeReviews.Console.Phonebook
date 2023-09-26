namespace PhoneBook;

class Category
{
    public int CategoryID { get; set; }
    public required string Name { get; set; }
    public ICollection<Contact> Contacts { get; } = new List<Contact>();
}