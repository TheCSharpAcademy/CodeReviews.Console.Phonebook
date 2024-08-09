using Spectre.Console;

namespace Phonebook.kwm0304.Views;

public class UserPrompts
{
  public static string StringPrompt(string input)
  {
    string answer = AnsiConsole.Ask<string>($"[bold blue]Contact {input}: [/]\n");
    if (!string.IsNullOrEmpty(answer) && !string.IsNullOrWhiteSpace(answer))
    {
      bool confirm = AnsiConsole.Confirm($"Confirm {input}: {answer}");
      if (confirm)
      {
        return answer;
      }
      else return StringPrompt(input);
    }
    AnsiConsole.MarkupLine($"[bold red]Contact {input} cannot be blank[/]");
    return StringPrompt(input);
  }
}