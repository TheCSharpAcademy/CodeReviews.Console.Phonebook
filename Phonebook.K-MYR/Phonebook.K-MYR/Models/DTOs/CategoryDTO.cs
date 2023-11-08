namespace Phonebook.K_MYR.Models;

internal class CategoryDTO
{
    public required string Name { get; set; }

    public List<ContactDTO> Contacts { get; set; } = new List<ContactDTO>();
}
