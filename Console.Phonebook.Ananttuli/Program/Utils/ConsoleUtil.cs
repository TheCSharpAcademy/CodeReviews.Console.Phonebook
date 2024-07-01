using Spectre.Console;

namespace Program.Utils;

public class ConsoleUtil
{
    public const string MenuBackButtonText = "[red]<- Back[/]";

    public static ConsoleKeyInfo PressAnyKeyToClear(string message = "Press any key to continue")
    {
        AnsiConsole.MarkupLine(message);
        var key = Console.ReadKey();
        Console.Clear();

        return key;
    }

}