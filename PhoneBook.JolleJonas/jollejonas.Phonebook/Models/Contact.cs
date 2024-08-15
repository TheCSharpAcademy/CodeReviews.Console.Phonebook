namespace jollejonas.Phonebook.Models;

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Note { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}