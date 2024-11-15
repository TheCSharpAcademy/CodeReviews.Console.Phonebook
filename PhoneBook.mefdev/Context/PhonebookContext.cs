using Microsoft.EntityFrameworkCore;
using PhoneBook.mefdev.Models;

namespace PhoneBook.mefdev.Context;

public class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Contacts)
            .HasForeignKey(c => c.CategoryId);

        modelBuilder.Entity<Contact>()
       .Property(c => c.Id)
       .ValueGeneratedOnAdd();

        modelBuilder.Entity<Category>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();
    }
}