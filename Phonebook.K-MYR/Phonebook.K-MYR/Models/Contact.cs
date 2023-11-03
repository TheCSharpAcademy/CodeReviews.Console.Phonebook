namespace Phonebook.K_MYR.Models;

internal class Contact
{
    public int Id { get; set; }
    public string Name {get; set;}
    public string EmailAdress { get; set; }
    public string PhoneNumber { get; set; }

    public Contact(string name, string emailAdress, string phoneNumber )
    {
        Name = name;
        EmailAdress = emailAdress;
        PhoneNumber = phoneNumber;
    }
}
