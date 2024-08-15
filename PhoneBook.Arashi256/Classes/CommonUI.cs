using Microsoft.IdentityModel.Tokens;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace PhoneBook.Arashi256.Classes
{
    internal class CommonUI
    {
        public static void Pause(string colour)
        {
            AnsiConsole.Markup($"[{colour}]Press any key to continue...[/]");
            Console.ReadKey(true);
        }
        public static int SelectNumberInRangeInput(string question, int min, int max)
        {
            int selectedValue = 0;
            var userInput = AnsiConsole.Ask<int>(question);
            selectedValue = userInput;
            if (selectedValue < min || selectedValue > max)
            {
                AnsiConsole.MarkupLine("[red]Invalid input. Please enter a value within the specified range.[/]");
            }
            return selectedValue;
        }

        public static string? GetStringInput(string message)
        {
            AnsiConsole.MarkupLine("[white]Enter '0' to cancel[/]");
            string input = AnsiConsole.Ask<string>(message);
            if (input == "0") return null;
            while (input.IsNullOrEmpty())
            {
                AnsiConsole.MarkupLine("\n\n[red]Invalid entry. Try again.[/]\n\n");
                input = AnsiConsole.Ask<string>(message);
            }
            return input;
        }

        public static string? GetValidatedStringInput(string question, string regex)
        {
            AnsiConsole.MarkupLine("[white]Enter '0' to cancel[/]");
            var validPattern = regex;
            string response = AnsiConsole.Prompt(
                new TextPrompt<string>(question)
                    .PromptStyle("cyan")
                    .ValidationErrorMessage("[red]That's not valid. Try again.[/]")
                    .Validate(answer =>
                    {
                        // Allow "0" as a cancel option or match the pattern.
                        return answer == "0" || Regex.IsMatch(answer, validPattern);
                    }));
            return response == "0" ? null : response;
        }

        public static string? GetPasswordInput(string question)
        {
            AnsiConsole.MarkupLine("[white]Enter '0' to cancel[/]");
            string input = AnsiConsole.Prompt(new TextPrompt<string>($"[white]{question}[/]: ").PromptStyle("grey50").Secret());
            return (input.IsNullOrEmpty() || input.Length == 0 || input.Equals("0") ? null : input);
        }
    }
}
