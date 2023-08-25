using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBookCarDioLogics.Models;

[Index(nameof(Name), IsUnique = true)]
internal class Contact
{
    [Key]
    public int ContactId { get; set; }   
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public int CategoryID { get; set; }
    [ForeignKey(nameof(CategoryID))]
    public Category Category { get; set; }  
}
