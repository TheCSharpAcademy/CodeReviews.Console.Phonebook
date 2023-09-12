namespace Phonebook;
public class Contact
{
    public int ContactID { get; set; }
    public string Name { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Address { get; set; } = "";
    public string City { get; set; } = "";
    public string State { get; set; } = "";
    public int ZipCode { get; set; }
    public string Email { get; set; } = "";
    public string Group { get; set; } = "";
    public DateTime LastAccess { get; set; }
}
