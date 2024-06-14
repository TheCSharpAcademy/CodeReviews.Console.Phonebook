namespace Phonebook.Entities;

public class ContactCategory
{
    public int Id { get; set; }
    public string CategoryName  { get; set; }
    public string? TestProperty { get; set; }
    
    public List<Contact> Contacts {get;} = new();
}