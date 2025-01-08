using System.ComponentModel.DataAnnotations.Schema;

namespace Console.Phonebook.Models;

internal class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public Category Category { get; set; }
}
