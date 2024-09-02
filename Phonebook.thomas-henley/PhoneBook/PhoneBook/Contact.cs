using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace PhoneBook;

public class PhonebookDbContext : DbContext
{
    private readonly string _cs;
    public DbSet<Contact> Contacts { get; set; }

    public PhonebookDbContext(string cs)
    {
        _cs = cs;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_cs);
    }
}

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}