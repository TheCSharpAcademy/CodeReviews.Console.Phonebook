using System.ComponentModel.DataAnnotations;

namespace TwilightSaw.Phonebook.Model;

public class Category(string name)
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = name;

    public virtual List<Contact>? contacts { get; set; }

    public override string ToString()
    {
        return Name;
    }
}