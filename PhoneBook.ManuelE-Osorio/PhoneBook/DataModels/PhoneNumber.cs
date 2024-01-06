namespace PhoneBookProgram;

public class PhoneNumber
{
    public int ContactId {get; set;}
    public Contact? Contact {get; set;}
    public int PhoneNumberId {get; set;}
    public string? CountryCode {get; set;}
    public string? LocalNumber {get; set;}

    public string? GetFullPhoneNumber()
    {
        return $"+{CountryCode}{LocalNumber}";
    }
}