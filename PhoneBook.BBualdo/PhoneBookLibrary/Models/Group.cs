using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PhoneBookLibrary.Models;

[Index(nameof(GroupId))]
public class Group
{
  [Key]
  public int GroupId { get; set; }

  [Required]
  public string Name { get; set; }
  public List<Contact> Contacts { get; set; }
}