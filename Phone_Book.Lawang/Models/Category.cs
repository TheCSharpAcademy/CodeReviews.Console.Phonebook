namespace Phone_Book.Lawang.Models;

public class Category
{
    public int Id { get; set; }
    public string CategoryName {get; set;} = null!;
    public ICollection<Contact> Contacts { get; set; } = null!;
}
