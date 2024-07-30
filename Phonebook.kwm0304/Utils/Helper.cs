using Phonebook.kwm0304.Views;
using Spectre.Console;

namespace Phonebook.kwm0304.Utils;

public class Helper
{
  public delegate bool ValidateFunction(string input);
  public static string GetInput(string input, ValidateFunction function)
  {
    string answer = UserPrompts.StringPrompt(input);
    bool isValid = function(answer);
    if (isValid)
    {
      return answer;
    }
    else
    {
      AnsiConsole.MarkupLine($"[bold red]Invalid {input}[/] Please try again");
      return GetInput(input, function);
    }
  }
  
}