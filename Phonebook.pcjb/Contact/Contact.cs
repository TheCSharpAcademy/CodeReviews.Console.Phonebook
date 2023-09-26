namespace PhoneBook;

class Contact
{
    public int ContactID { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int CategoryID { get; set; }
    public Category Category { get; set; } = null!;
}