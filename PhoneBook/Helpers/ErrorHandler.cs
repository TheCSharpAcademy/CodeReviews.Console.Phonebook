using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

internal class ErrorHandler
{
    internal static bool Success(Action action)
    {
        try
        {
            action();
            return true;
        }
        catch (DbUpdateException ex)
        {
            AnsiConsole.MarkupLine("[red]Database update error occurred:[/]");
            AnsiConsole.MarkupLine($"Details: [yellow]{ex.Message}[/]");
            DisplayInfoHelpers.PressAnyKeyToContinue();
            return false;
        }
        catch (SqlException ex)
        {
            AnsiConsole.MarkupLine("[red]SQL error occurred.[/]");
            AnsiConsole.MarkupLine($"Error number: [yellow]{ex.Number}[/]");
            AnsiConsole.MarkupLine($"Error state: [yellow]{ex.State}[/]");
            AnsiConsole.MarkupLine($"Details: [yellow]{ex.Message}[/]");
            DisplayInfoHelpers.PressAnyKeyToContinue();
            return false;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("[red]An error occurred.[/]");
            AnsiConsole.MarkupLine($"Details: [yellow]{ex.Message}[/]");
            DisplayInfoHelpers.PressAnyKeyToContinue();
            return false;
        }
    }
}
