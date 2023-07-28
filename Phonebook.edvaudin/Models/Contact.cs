using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phonebook.Models;

public class Contact
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(30)]
    [Column(TypeName = "varchar(30)")]
    public string PhoneNumber { get; set; } = string.Empty;
}
