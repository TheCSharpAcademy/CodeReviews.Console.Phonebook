using Phonebook.yemiodetola.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.EntityFrameworkCore.Design;

public class ContactsContext : DbContext
{
  public DbSet<Contact> Contacts { get; set; }
  // public DbSet<Category> Categories { get; set; }
  public ContactsContext(DbContextOptions<ContactsContext> options) : base(options) { }

    public ContactsContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Phonebook;User Id=sa;Password=<YourStrong@Passw0rd>;TrustServerCertificate=True");
  }
}