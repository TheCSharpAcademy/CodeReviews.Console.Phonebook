namespace Phonebook.kwm0304.Models;

public class Contact
{
  public int ContactId { get; set; }
  public string? ContactName { get; set; }
  public string? ContactEmail { get; set; }
  public string? ContactPhoneStr { get; set; }
  public long ContactPhoneInt { get; set; }
  public ContactGroup? Group { get; set; }
  public override string ToString()
  {
    return $"NAME: {ContactName} | NUMBER: {ContactPhoneStr} | EMAIL: {ContactEmail} | GROUP: {Group?.GroupName?.ToString()}";
  }
}
