using System.Text.RegularExpressions;

public static class Validator
{
  public static bool IsEmailValid(string email)
  {
    string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

    return Regex.IsMatch(email, emailPattern);
  }

  public static bool IsPhoneNumberValid(string phoneNumber)
  {
    string phonePattern = @"^\d{3}-\d{3}-\d{4}$";

    return Regex.IsMatch(phoneNumber, phonePattern);
  }
}
