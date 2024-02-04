
namespace Phonebook.frockett.DTOs;

public class ContactDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? ContactGroupName { get; set; }
}
