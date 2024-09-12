using Spectre.Console;

namespace PhoneBook;
public static class Prompter
{
    public static T EnumPrompt<T>() where T : struct, Enum
    {
        return AnsiConsole.Prompt<T>(
            new SelectionPrompt<T>()
                .Title("Choose an option: ")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(Enum.GetValues<T>()).UseConverter<T>(EnumHelper.GetTitle)
                );
    }

    public static (bool IsCancelled, string? Result) PromptWithValidation(string message, string defaultValue = null, params IValidator[] validations)
    {
        var textPrompt = new TextPrompt<string>($"{message}{CancelSetup.CancelPrompt}:")
                .PromptStyle("bold yellow");

        if (!string.IsNullOrEmpty(defaultValue))
        {
            textPrompt.DefaultValue(defaultValue);
        }


        textPrompt.Validate(input =>
        {
            // Checking first user cancel
            if (input == CancelSetup.CancelString)
                return ValidationResult.Success();

            foreach (var validationResult in validations)
            {
                var result = validationResult.Validate(input);
                if (!result.Successful) return result;
            }

            return ValidationResult.Success();
        });


        var result = AnsiConsole.Prompt<string>(textPrompt);
        if (CancelSetup.IsCancelled(result))
        {
            return (true, null);
        }

        return (false, result);
    }

    public static void PressKeyToContinuePrompt()
    {
        AnsiConsole.Write("Press any key to continue...");
        Console.ReadKey();
    }
}