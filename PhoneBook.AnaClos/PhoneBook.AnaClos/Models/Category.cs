using Microsoft.EntityFrameworkCore;

namespace PhoneBook.AnaClos.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Contact> Contacts { get; set; }
}
