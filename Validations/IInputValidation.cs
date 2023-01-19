namespace PhoneBookConsole.Validations;

public interface IInputValidation
{
    public bool ValidateName(string name);
    bool ValidatePhoneNumber(string phoneNumber);
    bool ValidateEmail(string email);
}