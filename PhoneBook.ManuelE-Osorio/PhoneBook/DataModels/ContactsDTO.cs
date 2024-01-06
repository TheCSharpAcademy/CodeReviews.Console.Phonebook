namespace PhoneBookProgram;

public class ContactDTO(Contact contact)
{
    public int ContactId {get;} = contact.ContactId;
    public string? ContactName {get;} = contact.ContactName;
    public string? Category {get;} = contact.Category;
}