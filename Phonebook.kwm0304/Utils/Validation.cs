using System.Text.RegularExpressions;
using Spectre.Console;

namespace Phonebook.kwm0304.Utils;

public class Validation
{
  public static bool IsValidEmail(string email)
  {
    return Regex.IsMatch(email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
  }
  public static bool IsValidNumber(long phoneNumber)
  {
    string numStr = phoneNumber.ToString();
    if (numStr.Length == 11)
    {
      char first = numStr[0];
      int digit = Convert.ToInt32(first.ToString());
      if (digit == 1 && ConfirmCountryCode(digit))
      {
        return true;
      }
      return false;
    }
    else if (numStr.Length == 10)
    {
      return true;
    }
    return false;
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

  public static string NormalizePhoneNumberStr(string phoneNumber)
  {
    string pattern = @"\D";
    return Regex.Replace(phoneNumber, pattern, "");

  }
  
  public static long NormalizePhoneNumberInt(string phoneNumber)
  {
    string pattern = @"\D";
    string formatted = Regex.Replace(phoneNumber, pattern, "");
    return Convert.ToInt64(formatted);
  }

  public static string FormatContactNumberStr(string number)
  {
    string normalized = NormalizePhoneNumberStr(number);
    if (normalized.Length == 10)
    {
      return $"({normalized.Substring(0, 3)}) {normalized.Substring(3, 3)}-{normalized.Substring(6, 4)}";
    }
    else
    {
      return $"+1 ({normalized.Substring(1, 3)}) {normalized.Substring(4, 3)}-{normalized.Substring(7, 4)}";
    }
  }

  public static bool IsValidPhoneNumber(string number)
  {
    bool initialCheck = PreliminaryValidation(number);
    if (initialCheck)
    {
      long initialNumber = NormalizePhoneNumberInt(number);
      return IsValidNumber(initialNumber);
    }
    return false;
  }
}