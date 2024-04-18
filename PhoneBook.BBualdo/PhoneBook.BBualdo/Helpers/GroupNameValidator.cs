using Spectre.Console;

namespace PhoneBook.BBualdo.Helpers;

public static class GroupNameValidator
{
  public static bool IsValid(string groupName)
  {
    if (int.TryParse(groupName, out _))
    {
      AnsiConsole.Markup("[red]Name can't be numeric. [/]\n");
      return false;
    }

    return true;
  }
}