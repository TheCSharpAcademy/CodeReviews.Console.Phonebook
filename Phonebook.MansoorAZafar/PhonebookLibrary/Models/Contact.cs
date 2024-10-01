namespace PhonebookLibrary.Models;

internal class Contact
{
    public static string[] Headers = {"Id", "Name", "Email", "PhoneNumber" };
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public Contact(string name, string email, string phoneNumber)
    {
        this.Name = name;
        this.Email = email;
        this.PhoneNumber = phoneNumber;
    }

    public Contact() { }
}
