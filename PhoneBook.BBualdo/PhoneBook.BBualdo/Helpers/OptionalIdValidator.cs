using Spectre.Console;

namespace PhoneBook.BBualdo.Helpers;

public static class OptionalIdValidator
{
  public static bool IsValid(string id)
  {
    if (!int.TryParse(id, out _))
    {
      AnsiConsole.Markup("[red]ID must be valid numeric value.[/]\n");
      return false;
    }

    return true;
  }
}