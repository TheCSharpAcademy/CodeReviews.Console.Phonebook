namespace PhoneBookProgram;

public class Contact
{
    public int ContactId {get; set;}
    public string? ContactName {get; set;}
    public List<PhoneNumber> PhoneNumbers {get; set;} = [];
    public List<Email> Emails {get; set;} = [];
    public string? Category {get; set;}
}