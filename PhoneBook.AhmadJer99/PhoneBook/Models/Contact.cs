namespace PhoneBook.Models;

public class Contact
{
    public int ContactId { get; set; }
    public string? ContactName { get; set; }
    public string? ContactTitle { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public bool ContactEmailStatus { get; set; }
    public bool ContactPhoneStatus { get; set; }
}