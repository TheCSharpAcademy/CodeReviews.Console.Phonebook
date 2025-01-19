namespace Phonebook.yemiodetola.Models;

public class Contact
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string PhoneNumber { get; set; }

  public bool validatePhoneNumber(string phoneNumber)
  {
    return !string.IsNullOrEmpty(phoneNumber)
      && phoneNumber.Length == 11
      && phoneNumber.StartsWith("0")
      && phoneNumber.All(char.IsDigit);
  }


  public bool validateEmailAddress(string email)
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
}