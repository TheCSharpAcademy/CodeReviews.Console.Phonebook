using Console.Phonebook.App.Enums;

namespace Console.Phonebook.App.Entities;

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public Categories Categories { get; set; }
}


//.add()
//.save()
//.update()