namespace PhoneBookConsole.Input;

public class UserInput
{
    public string GetInput()
    {
       return Console.ReadLine().Trim();
    }
}