using Spectre.Console;

namespace Phonebook.K_MYR
{
    internal static class Helpers
    {
        internal static void WriteMessageAndWait(string message)
        {
            AnsiConsole.Write(new Panel(message).BorderColor(Color.DarkOrange3_1));
            Console.ReadKey();
        }
    }
}
