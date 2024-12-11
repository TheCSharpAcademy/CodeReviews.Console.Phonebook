using Spectre.Console;

namespace Phonebook.Utilities;

public static class Util
{
    internal static void AskUserToContinue()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    internal static bool ReturnToMenu()
    {
        var input = AnsiConsole.Prompt(
            new TextPrompt<string>("Type [green]0[/] to return to the menu or press enter to continue:")
                .PromptStyle("yellow")
                .AllowEmpty());
            ;

        if (input == "0")
        {
            return true;
        }
        return false;
    }

    internal static string Capitalise(string text) => char.ToUpper(text[0]) + text.Substring(1).ToLower();
}