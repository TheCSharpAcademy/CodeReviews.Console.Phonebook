using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Phonebook;

[Index(nameof(Name), IsUnique = true)]
public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [StringLength(450)]
    [Required]
    public string Name { get; set; } = string.Empty;

    public virtual List<Contact> Contacts { get; set; } = default!;
}