using Microsoft.EntityFrameworkCore;
using PhoneBook.Doc415.Models;
namespace PhoneBook.Doc415.Context;

internal class PhoneBookContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Data source to be replaced with config connection string.
        optionsBuilder.UseSqlServer($"Server=(localdb)\\MSSQLLocalDB;Initial Catalog=PhoneBook; Integrated Security=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .HasKey(contact => new { contact.ContactID });
    }
    public DbSet<Contact> Contacts { get; set; }
}
