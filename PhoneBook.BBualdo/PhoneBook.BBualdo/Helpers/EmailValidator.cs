using Spectre.Console;
using System.Text.RegularExpressions;

namespace PhoneBook.BBualdo.Helpers;

public static class EmailValidator
{
  public static bool IsValid(string email)
  {
    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

    if (!regex.IsMatch(email))
    {
      AnsiConsole.Markup("[red]Invalid email format! [/]\n");
      return false;
    }

    return true;
  }
}