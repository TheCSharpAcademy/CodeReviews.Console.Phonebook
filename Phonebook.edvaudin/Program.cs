namespace Phonebook;

internal class Program
{
    static bool endApp = false;
    static void Main(string[] args)
    {
        UserMenu.DisplayTitle();

        while (!endApp)
        {
            UserMenu.DisplayOptionsMenu();
            string userInput = UserInput.GetUserOption();
            UserController.ProcessInput(userInput);
        }
        ExitApp();
    }
    public static void SetEndAppToTrue() => endApp = true;

    private static void ExitApp() => Environment.Exit(0);
}