namespace jollejonas.Phonebook.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Contact> Contacts { get; set; }
}
