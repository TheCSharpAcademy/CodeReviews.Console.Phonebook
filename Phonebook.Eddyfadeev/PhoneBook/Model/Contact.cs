using PhoneBook.Interfaces.Model;

namespace PhoneBook.Model;

internal class Contact : IContact
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Group { get; set; }
    
    public override string ToString() => 
        $"""
         Name: {FirstName} {LastName}
           Phone: {PhoneNumber}
           Email: {Email}
           Group: {Group}
         """;
}