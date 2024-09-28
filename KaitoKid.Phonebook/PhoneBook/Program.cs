using Microsoft.Extensions.Configuration;
using PhoneBook;

public class Program
{
    public static void Main()
    {
        UserInput userInput = new ();
        userInput.MainMenuChoice();
    }
}