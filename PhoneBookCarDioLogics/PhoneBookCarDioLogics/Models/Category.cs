using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PhoneBookCarDioLogics.Models;

[Index(nameof(CategoryName), IsUnique = true)]
internal class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    public string CategoryName { get; set; }
    public List<Contact> Contacts { get; set; }
}
