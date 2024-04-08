using Microsoft.EntityFrameworkCore;
using PhoneBook.Cactus.DataModel;

namespace PhoneBook.Cactus.DB;

public class ContactContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source = contact.db");

}

