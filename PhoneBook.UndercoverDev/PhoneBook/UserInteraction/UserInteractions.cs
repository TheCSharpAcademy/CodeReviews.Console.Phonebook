using Spectre.Console;

namespace PhoneBook.UserInteraction
{
    public class UserInteractions
    {
        public static void Exit()
        {
            AnsiConsole.WriteLine("Exiting from application\n");
            Environment.Exit(0);
        }
    }
}