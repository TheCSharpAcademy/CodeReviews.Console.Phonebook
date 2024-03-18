using Microsoft.EntityFrameworkCore;

namespace PhoneBook.Dejmenek.Models;

[Index(nameof(PhoneNumber), IsUnique = true)]
public class Contact
{
    public int Id { get; set; }
    public int? CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public Category? Category { get; set; }
}

public class ContactDTO
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
}