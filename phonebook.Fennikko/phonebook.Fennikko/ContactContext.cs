using Microsoft.EntityFrameworkCore;
using phonebook.Fennikko.Models;

namespace phonebook.Fennikko;

public class ContactContext : DbContext
{
    public DbSet<ContactInfo> Contacts { get; set; }

    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer($"Server=localhost;Integrated Security=true;Database=Contacts;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactInfo>()
            .HasKey(c => new { c.ContactId });

        modelBuilder.Entity<ContactInfo>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Contacts)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}