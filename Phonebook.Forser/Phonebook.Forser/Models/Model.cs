using Microsoft.EntityFrameworkCore;

public class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\;Database=Phonebook;Trusted_Connection=True;
        MultipleActiveResultSets=true;Encrypt=False");
    }
}
public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; }

    public List<Category> Categories { get; set; }
    public Contact() {}
    public Contact(string name, string? email, string phoneNumber)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}
public class Category
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public int ContactId { get; set; }
    public Contact Contact { get; set; }
}