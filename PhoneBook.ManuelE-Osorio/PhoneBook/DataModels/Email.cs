namespace PhoneBookProgram;

public class Email
{
    public int ContactId {get; set;}
    public Contact? Contact{get; set;}
    public int EmailId {get; set;}
    public string? LocalName {get; set;}
    public string? DomainName {get; set;}

    public string? GetEmail()
    {
        return $"{LocalName}@{DomainName}";
    }
}