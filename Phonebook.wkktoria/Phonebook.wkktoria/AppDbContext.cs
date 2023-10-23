using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Phonebook.wkktoria.Models;

namespace Phonebook.wkktoria;

public class AppDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany<Contact>(ca => ca.Contacts)
            .WithOne(co => co.Category)
            .HasForeignKey(co => co.CategoryId);

        modelBuilder.Entity<Contact>()
            .HasOne(co => co.Category)
            .WithMany(ca => ca.Contacts)
            .HasForeignKey(co => co.CategoryId);

        modelBuilder.Entity<Category>()
            .HasData(new List<Category>
                {
                    new()
                    {
                        Id = 1,
                        Name = "Family"
                    },
                    new()
                    {
                        Id = 2,
                        Name = "Friends"
                    },
                    new()
                    {
                        Id = 3,
                        Name = "Work"
                    }
                }
            );

        modelBuilder.Entity<Contact>()
            .HasData(new List<Contact>
            {
                new()
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "John",
                    Email = "john@email.com",
                    PhoneNumber = "123-123-123"
                },
                new()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Adam",
                    Email = "adam@email.com",
                    PhoneNumber = "111-111-111"
                },
                new()
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Anne",
                    Email = "anne@email.com",
                    PhoneNumber = "321-321-321"
                },
                new()
                {
                    Id = 4,
                    CategoryId = 3,
                    Name = "Victoria",
                    Email = "victoria@email.com",
                    PhoneNumber = "333-333-333"
                }
            });
    }
}