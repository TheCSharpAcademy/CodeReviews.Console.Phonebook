
namespace Phonebook.frockett.Models;

public class Contact
{
    public int ContactId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int? GroupId { get; set; }
    public ContactGroup? ContactGroup { get; set; }
}
