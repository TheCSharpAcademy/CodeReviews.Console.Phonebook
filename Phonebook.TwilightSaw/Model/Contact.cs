using System.ComponentModel.DataAnnotations;

namespace TwilightSaw.Phonebook.Model;

public class Contact(string name, string email, string phoneNumber)
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public string PhoneNumber { get; set; } = phoneNumber;
    
    public virtual List<Category>? categories { get; set; }

    public override string ToString()
    {
        return Name == "Return" ? $"{Name}" : $"{Name} {PhoneNumber} {Email}";
    }
}