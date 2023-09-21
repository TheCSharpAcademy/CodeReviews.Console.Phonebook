namespace PhoneBook;

class Contact
{
    public int Id { get; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public Contact(int id, string name)
    {
        Id = id;
        Name = name;
    }
}