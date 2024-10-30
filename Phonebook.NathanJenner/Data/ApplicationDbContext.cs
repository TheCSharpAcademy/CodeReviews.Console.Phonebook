using Console.Phonebook.App.Entities;
using Microsoft.EntityFrameworkCore;

namespace Console.Phonebook.App.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\phonebook;Database=PhoneContacts;Trusted_Connection=True;");
    }
}
