using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phone_Book.Lawang.Models;

public class Contact
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int CategoryID { get; set; }
    [ForeignKey("CategoryID")]
    public Category Category { get; set; } = null!;
}
