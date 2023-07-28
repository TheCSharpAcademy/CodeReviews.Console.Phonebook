using Kmakai.PhoneBook.Models;
using Microsoft.EntityFrameworkCore;

namespace Kmakai.PhoneBook.Data;

public class AppDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.;Trusted_Connection=True;database=PhoneBook;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>().HasData(
                       new Contact { Id = 1, Name = "John Doe", PhoneNumber = "1234567890", Email = "example@gmail.com" });

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Family" },
            new Category { Id = 2, Name = "Friends" },
            new Category { Id = 3, Name = "Work" },
            new Category { Id = 4, Name = "Other" });
    }
}
