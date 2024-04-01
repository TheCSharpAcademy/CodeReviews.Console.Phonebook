namespace phonebook.Fennikko.Models;

public class ContactInfo
{
    public int ContactId { get; set; }

    public string ContactName { get; set; }

    public string ContactEmail { get; set; }

    public string ContactPhone { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; }
}