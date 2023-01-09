using System.Text.RegularExpressions;

namespace PhoneBookConsole.Validations;

public class InputValidation : IInputValidation
{
    public bool ValidateName(string name)
    {
        return !string.IsNullOrWhiteSpace(name);
    }

    public bool ValidatePhoneNumber(string phoneNumber)
    {
        var pattern = @"^\+\d{1,3} \d{10,10}";
        var regex = new Regex(pattern);
        
        return !string.IsNullOrWhiteSpace(phoneNumber) && regex.IsMatch(phoneNumber);
    }

    public bool ValidateEmail(string email)
    {
        var pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                      + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                      + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        var regex = new Regex(pattern);

        return !string.IsNullOrWhiteSpace(email) && regex.IsMatch(email);
    }
}