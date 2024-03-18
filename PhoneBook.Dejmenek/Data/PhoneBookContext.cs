using Microsoft.EntityFrameworkCore;
using PhoneBook.Dejmenek.Models;
using System.Configuration;

namespace PhoneBook.Dejmenek.Data;

public class PhoneBookContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["LocalDbConnection"].ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .HasOne(c => c.Category)
            .WithOne(cat => cat.Contact)
            .HasForeignKey<Contact>(c => c.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
