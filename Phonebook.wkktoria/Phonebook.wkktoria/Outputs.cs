namespace Phonebook.wkktoria;

public static class Outputs
{
    public static void ExceptionMessage(string message)
    {
        WriteMessage(message, ConsoleColor.Red);
    }

    public static void InvalidInputMessage(string message)
    {
        WriteMessage(message, ConsoleColor.DarkRed);
    }

    private static void WriteMessage(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}