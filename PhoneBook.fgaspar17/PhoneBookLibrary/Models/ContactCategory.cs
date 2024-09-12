namespace PhoneBookLibrary;

public class ContactCategory
{
    public int ContactId { get; set; }
    public int CategoryId { get; set; }
    public Contact Contact { get; set; }
    public Category Category { get; set; }
}