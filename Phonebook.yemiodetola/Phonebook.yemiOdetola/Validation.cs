using Spectre.Console;
using Phonebook.yemiOdetola;

namespace Phonebook.yemiOdetola;

public class Validation
{

  public static bool validatePhoneNumber(string phoneNumber)
  {
    return !string.IsNullOrEmpty(phoneNumber)
      && phoneNumber.Length == 11
      && phoneNumber.StartsWith("0")
      && phoneNumber.All(char.IsDigit);
  }


  public static bool validateEmailAddress(string email)
  {
    if (String.IsNullOrEmpty(email))
    {
      return false;
    }
    int atIndex = email.IndexOf('@');
    int dotIndex = email.LastIndexOf('.');

    return atIndex > 0
      && dotIndex > atIndex + 1
      && dotIndex < email.Length - 1;

  }

  public static string GetAndValidatePhone()
  {
    var phone = AnsiConsole.Ask<string>("Enter phone number:");
    while (!validatePhoneNumber(phone))
    {
      AnsiConsole.MarkupLine("[bold red]Invalid phone number. Please try again.[/]");
      phone = AnsiConsole.Ask<string>("Enter phone number:");
    }

    return phone;
  }

  public static string GetAndValidateEmail()
  {
    var email = AnsiConsole.Ask<string>("Enter email address:");
    while (!validateEmailAddress(email))
    {
      AnsiConsole.MarkupLine("[bold red]Invalid email address. Please try again.[/]");
      email = AnsiConsole.Ask<string>("Enter email address:");
    }

    return email;
  }
}