namespace PhoneBook.Interfaces.Model;

internal interface IContact
{
    int Id { get; set; }
    string? FirstName { get; set; }
    string? LastName { get; set; }
    string? PhoneNumber { get; set; }
    string? Email { get; set; }
    string? Group  { get; set; }
}