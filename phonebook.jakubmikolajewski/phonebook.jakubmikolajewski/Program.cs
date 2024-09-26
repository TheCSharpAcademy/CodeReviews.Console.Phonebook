using Phonebook.Library;

internal class Program
{
    static bool exit;

    private static void Main(string[] args)
    {
        while (!exit)
        {
            exit = UserInput.SwitchMenuChoice(UserInput.ShowMenu());
        }    
    }
}