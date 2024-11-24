using Spectre.Console;

namespace TwilightSaw.Phonebook.Helpers;

public class Validation
{
    public static string Validate(Action action, bool getMessage)
    {
        try
        {
            action();
        }
        catch (Exception e)
        {
            return getMessage ? e.Message : "";
        }
        return "Executed successfully";
    }

    public static string Validate<T>(T action, bool getMessage, out T back)
    {
        try
        {
            back = action;
        }
        catch (Exception e)
        {
            back = default;
            return getMessage ? e.Message : "";
        }
        return "Executed successfully";
    }

    public static void EndMessage(string? message)
    {
        if (message != null)
        {
            AnsiConsole.MarkupLine($"[olive]{message}[/]");
            AnsiConsole.Markup($"[grey]Press any key to continue.[/]");
            Console.ReadKey(intercept: true);
        }
        Console.Clear();
    }
}