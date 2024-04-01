namespace phonebook.Fennikko.Models;

public class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; }

    public List<ContactInfo> Contacts { get; set; }
}