using PhoneValidation = PhoneNumbers;

namespace PhoneBookProgram;

public class PhoneNumber
{
    public int ContactId {get; set;}
    public int PhoneNumberId {get; set;}
    public string? CountryCode {get; set;}
    public string? LocalNumber {get; set;}

    public static PhoneNumber FromCsv(string phoneLine, int contactId)
    {
        string[] data = phoneLine.Split(',');
        string? errorMessage = InputValidation.PhoneNumberValidation(data[1]);
    
        if(errorMessage == null)
        { 
            var phoneNumberUtil = PhoneValidation.PhoneNumberUtil.GetInstance();
            var phoneNumber = phoneNumberUtil.Parse(data[1], null);
            var phone = new PhoneNumber
            {
                ContactId = contactId,
                CountryCode = phoneNumber.CountryCode.ToString(),
                LocalNumber = phoneNumber.NationalNumber.ToString()
            };
            return phone;
        }
        else
            throw new Exception($"Error: Invalid Phone Number \"{data[1]}\". {errorMessage}");
    }
   
    public string? GetFullPhoneNumber()
    {
        return $"+{CountryCode} {LocalNumber}";
    }
}