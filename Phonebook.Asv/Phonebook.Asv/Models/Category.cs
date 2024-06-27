namespace Phonebook.Models;

internal class Category
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;
    public ICollection<Contact> Contact { get; set; } = null!;
}