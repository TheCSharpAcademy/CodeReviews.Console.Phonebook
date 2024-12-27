using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.AnaClos.Models;
[Index(nameof(Name), IsUnique = true)]
public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCategory { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Contact> Contacts { get; set; }
}
