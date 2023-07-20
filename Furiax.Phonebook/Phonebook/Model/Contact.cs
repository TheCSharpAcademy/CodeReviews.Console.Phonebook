using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phonebook.Model;
[Index(nameof(Name), IsUnique = true)]
internal class Contact
{
    [Key]
    public int ContactId { get; set; }
    [Required]
    public string  Name { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string EmailAddress { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }
}
