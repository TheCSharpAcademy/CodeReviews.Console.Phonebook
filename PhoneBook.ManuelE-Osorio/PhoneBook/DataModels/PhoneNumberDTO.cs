namespace PhoneBookProgram;

public class PhoneNumberDTO(PhoneNumber phoneNumber)
{
    public string? PhoneNumber {get; set;} = phoneNumber.GetFullPhoneNumber();
}