using Spectre.Console;

namespace PhoneBook.BBualdo.Helpers;

public static class ContactNameValidator
{
  public static bool IsValid(string contactName)
  {
    if (int.TryParse(contactName, out _))
    {
      AnsiConsole.Markup("[red]Name can't be numeric. [/]\n");
      return false;
    }

    return true;
  }
}