using Spectre.Console;

namespace PhoneBook.DouglasFir.Utilities;

public static class UserInput
{
    public static void PauseForContinueInput()
    {
        AnsiConsole.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public static bool ConfirmAction(string actionPromptMessage)
    {
        if (!AnsiConsole.Confirm(actionPromptMessage))
        {
            Util.DisplayCancellationMessage("Operation cancelled.");
            PauseForContinueInput();
            return false;
        }

        return true;
    }

    public static string PromptForNonEmptyString(string promptMessage)
    {
        string userInput = AnsiConsole.Prompt(
            new TextPrompt<string>(promptMessage)
                .PromptStyle("yellow")
                .Validate(input =>
                {
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        return ValidationResult.Success();
                    }
                    else
                    {
                        var errorMessage = "[red]Input cannot be empty.[/]";
                        return ValidationResult.Error(errorMessage);
                    }
                }));

        return userInput.Trim();
    }

    public static int PromptForInteger(string promptMessage)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<int>(promptMessage)
                .Validate(input =>
                {
                    if (!int.TryParse(input.ToString().Trim(), out int parsedQuantity))
                    {
                        return ValidationResult.Error("[red]Id's must be a postive integer value.[/]");
                    }

                    if (parsedQuantity <= 0)
                    {
                        return ValidationResult.Error("[red]Id's must be a postive integer value.[/]");
                    }

                    return ValidationResult.Success();
                }));
    }
}
