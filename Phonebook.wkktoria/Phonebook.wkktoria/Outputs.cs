namespace Phonebook.wkktoria;

public static class Outputs
{
    public static void InvalidInputMessage(string message)
    {
        WriteMessage(message, ConsoleColor.Red);
    }

    private static void WriteMessage(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}