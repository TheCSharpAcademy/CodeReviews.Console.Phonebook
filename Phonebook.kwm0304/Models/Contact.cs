namespace Phonebook.kwm0304.Models;

public class Contact
{
  public int ContactId { get; set; }
  public string? ContactName { get; set; }
  public string? ContactEmail { get; set; }
  public string? ContactPhoneStr { get; set; }
  public int ContactPhoneInt { get; set; }
  public ContactGroup? Group { get; set; }
}
