using Spectre.Console;

namespace PhoneBook.Services;

/// <summary>
/// A static helper service that provides common utility functions for the PhoneBook application.
/// </summary>
internal static class HelperService
{
    /// <summary>
    /// Prompts the user to press any key to continue by displaying a message and waiting for a key press.
    /// </summary>
    public static void PressAnyKey()
    {
        AnsiConsole.MarkupLine(PressAnyKeyOption);
        Console.ReadKey();
    }
}