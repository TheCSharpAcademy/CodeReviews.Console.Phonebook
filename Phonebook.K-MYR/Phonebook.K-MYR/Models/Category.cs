using System.ComponentModel.DataAnnotations;

namespace Phonebook.K_MYR.Models;

internal class Category
{
    public int CategoryId { get; set; }
    [StringLength(50)]
    public required string Name { get; set; }
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}
