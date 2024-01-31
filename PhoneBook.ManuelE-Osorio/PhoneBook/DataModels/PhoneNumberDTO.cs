namespace PhoneBookProgram;

public class PhoneNumberDto(PhoneNumber phoneNumber)
{
    public string? Selected { get; set; } = "[ ]";
    public string? PhoneNumber {get; set;} = phoneNumber.GetFullPhoneNumber();
}