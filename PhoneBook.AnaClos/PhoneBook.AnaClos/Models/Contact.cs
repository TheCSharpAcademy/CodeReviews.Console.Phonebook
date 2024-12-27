using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace PhoneBook.AnaClos.Models;

[Index(nameof(Name), IsUnique = true)]
public class Contact
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int IdCategory { get; set; }
    [ForeignKey("IdCategory")]
    public virtual Category Category { get; set; }
}