namespace Phonebook.MartinL_no.Models;

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int CategoryId { get; set; }
}