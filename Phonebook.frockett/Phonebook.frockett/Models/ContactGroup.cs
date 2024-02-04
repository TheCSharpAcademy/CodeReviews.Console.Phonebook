
namespace Phonebook.frockett.Models;

public class ContactGroup
{
    public int ContactGroupId { get; set; }
    public string Name { get; set; }
    public List<Contact>? Contacts { get; set; }
}
