namespace PhoneBookProgram;

public class ContactDto(Contact contact)
{
    public int ContactId { get; } = contact.ContactId;
    public string? ContactName { get; } = contact.ContactName;
    public string? Category { get; } = contact.Category;
}