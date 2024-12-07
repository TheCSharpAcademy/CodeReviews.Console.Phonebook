namespace PhoneBook.AnaClos.Models;

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int IdCategory { get; set; }
    public Category Category { get; set; }
}