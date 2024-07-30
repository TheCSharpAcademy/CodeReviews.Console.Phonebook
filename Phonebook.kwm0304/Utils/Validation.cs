
using System.Text.RegularExpressions;
using Spectre.Console;

namespace Phonebook.kwm0304.Utils;

public class Validation
{
  public static bool IsValidEmail(string email)
  {
    return true;
  }
  public static bool IsValidNumber(int phoneNumber)
  {
    if (phoneNumber == 11)
    {
      string numStr = phoneNumber.ToString();
      char first = numStr[0];
      int digit = Convert.ToInt32(first);
      if (digit == 1 && ConfirmCountryCode(digit))
      {
        return true;
      }
      return false;
    }
    if (phoneNumber < 10 || phoneNumber > 11)
    {
      return false;
    }
    return true;
  }

  private static bool ConfirmCountryCode(int digit)
  {
    return AnsiConsole.Confirm($"Is {digit} the country code?");
  }

  public static bool PreliminaryValidation(string phoneNumber)
  {
    if (string.IsNullOrEmpty(phoneNumber) ||
        string.IsNullOrWhiteSpace(phoneNumber) ||
        phoneNumber.Length < 7)
    {
      return false;
    }
    return true;
  }
  //removes anything not a number from string 
  public static string NormalizePhoneNumberStr(string phoneNumber)
  {
    string pattern = @"\D";
    return Regex.Replace(phoneNumber, pattern, "");

  }
  public static int NormalizePhoneNumberInt(string phoneNumber)
  {
    string pattern = @"\D";
    string formatted = Regex.Replace(phoneNumber, pattern, "");
    return Convert.ToInt32(formatted);
  }
  //Format string number for saving
  public static string FormatContactNumberStr(string number)
  {
    if (number.Length == 10)
    {
      return $"({number.Substring(0, 3)}) {number.Substring(3, 3)}-{number.Substring(6, 4)}";
    }
    else
    {
      return $"+1 ({number.Substring(1, 3)}) {number.Substring(4, 3)}-{number.Substring(7, 4)}";
    }
  }

  public static bool IsValidPhoneNumber(string number)
  {
    bool initialCheck = PreliminaryValidation(number);
    if (initialCheck)
    {
      int initialNumber = NormalizePhoneNumberInt(number);
      return IsValidNumber(initialNumber);
    }
    return false;
  }
}