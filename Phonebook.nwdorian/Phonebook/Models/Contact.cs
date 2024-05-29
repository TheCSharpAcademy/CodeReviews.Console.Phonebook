using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phonebook.Models;
internal class Contact
{
    public int Id { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string? Name { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string? Email { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string? PhoneNumber { get; set; }
}
