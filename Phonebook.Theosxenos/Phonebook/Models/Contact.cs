using Microsoft.EntityFrameworkCore;

namespace Phonebook.Models;

[Index(nameof(Name), nameof(Email), nameof(PhoneNumber), IsUnique = true)]
public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public override string ToString()
    {
        return Name;
    }
}