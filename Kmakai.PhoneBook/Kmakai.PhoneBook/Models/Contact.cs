using System.ComponentModel.DataAnnotations.Schema;

namespace Kmakai.PhoneBook.Models;

public class Contact
{
    public int Id { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
