namespace Phonebook.Entities;

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    
    public int ContactCategoryId { get; set; }
    public ContactCategory ContactCategory { get; set; }

}