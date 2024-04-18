using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBookLibrary.Models;

[Index(nameof(Name))]
public class Contact
{
  [Key]
  public int ContactId { get; set; }

  [Required]
  public string Name { get; set; }
  public string? Email { get; set; }

  [Required]
  public string PhoneNumber { get; set; }
  public int? GroupId { get; set; }

  [ForeignKey(nameof(GroupId))]
  public Group? Group { get; set; }
}