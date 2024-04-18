using Spectre.Console;
using System.Text.RegularExpressions;

namespace PhoneBook.BBualdo.Helpers;

public static class PhoneNumberValidator
{
  public static bool IsValid(string phoneNumber)
  {
    Regex regex = new Regex("^\\d{3}-\\d{3}-\\d{3}$");

    if (!regex.IsMatch(phoneNumber))
    {
      AnsiConsole.Markup("[red]Invalid phone number format.[/] Valid format is [cyan1]000-000-000[/].\n");
      return false;
    }

    return true;
  }
}