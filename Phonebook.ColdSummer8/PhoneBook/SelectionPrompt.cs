using Spectre.Console;

namespace PhoneBook;
internal class SelectionPrompt
{
    public static string Selection(Dictionary<string, int> choices)
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .AddChoices(choices.Keys)
            .HighlightStyle(Style.Parse("red"))
            );
    }
}
