namespace Phonebook.MartinL_no.Models;

internal class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int? CategoryId { get; set; }
    public Category? category { get; set; } = null;
}