namespace Phonebook.wkktoria.Models.Dtos;

public class ContactDto
{
    public string? Name { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public Category? Category { get; init; }

    public override string ToString()
    {
        return $"{Name} | {Email} | {PhoneNumber}";
    }
}