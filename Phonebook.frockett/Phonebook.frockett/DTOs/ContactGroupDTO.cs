namespace Phonebook.frockett.DTOs;

public class ContactGroupDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ContactDTO>? Contacts { get; set; } = new List<ContactDTO>();
}
