namespace PhoneBookConsole.Input;

public interface IUserInput
{
    public string GetInput();
    public string GetName();
    public string GetPhoneNumber();
    public string GetEmail();
}