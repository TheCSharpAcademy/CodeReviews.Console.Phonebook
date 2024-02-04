using Microsoft.EntityFrameworkCore;
using Phonebook.frockett.Models;

namespace Phonebook.frockett.DataLayer;

public class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ContactGroup> ContactGroup { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contact>()
        .HasOne(c => c.ContactGroup) 
        .WithMany(g => g.Contacts) 
        .HasForeignKey(c => c.GroupId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer(@"Server=Crockett;Database=PhoneBookDb;Trusted_Connection=True;TrustServerCertificate=True");
}
