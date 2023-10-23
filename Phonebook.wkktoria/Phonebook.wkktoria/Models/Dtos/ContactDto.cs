namespace Phonebook.wkktoria.Models.Dtos;

public class ContactDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Category Category { get; set; }

    public override string ToString()
    {
        return $"{Name} | {Email} | {PhoneNumber}";
    }
}