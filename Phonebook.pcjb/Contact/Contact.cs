namespace PhoneBook;

class Contact
{
    public int ContactID { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}