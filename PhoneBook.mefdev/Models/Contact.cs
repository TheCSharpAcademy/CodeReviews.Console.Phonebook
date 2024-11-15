namespace PhoneBook.mefdev.Models;

public class Contact
{
    public int Id { get; set; }
    public string Name {get; set;} = null!;
    public string Phone {get; set;} = null!;
    public string Email {get; set;} = null!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}