using System.ComponentModel.DataAnnotations.Schema;

namespace Phonebook.Models;

internal class Contact
{
    public int Id { get; set; }
    public string ContactName { get; set; } = null!;
    public string ContactPhoneno { get; set; } = null!;
    public string ?ContactEmailid { get; set; }
    // Foreign key for Category
    public int CategoryId { get; set; }
    // Navigation property
    [ForeignKey("CategoryId")]
    public Category Category { get; set; } = null!;
}