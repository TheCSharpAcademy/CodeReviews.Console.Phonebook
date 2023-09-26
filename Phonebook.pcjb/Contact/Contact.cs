namespace PhoneBook;

class Contact
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MobileNumber { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}