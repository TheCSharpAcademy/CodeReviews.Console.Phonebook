using Microsoft.EntityFrameworkCore;
using PhoneBook.Doc415.Models;
namespace PhoneBook.Doc415.Context;

internal class PhoneBookContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Data source to be replaced with config connection string.
        string projPath = Path.GetFullPath(@"C:\Users\sipah\Desktop\serdar\projeler\PhoneBook.Doc415\");
        optionsBuilder.UseSqlServer($"Server=(localdb)\\MSSQLLocalDB;Initial Catalog=PhoneBook; Integrated Security=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .HasKey(contact => new { contact.ContactID });
    }
    public DbSet<Contact> Contacts { get; set; }
}
