using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Phonebook.wkktoria.Models;

namespace Phonebook.wkktoria;

public class AppDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .HasData(new List<Contact>
            {
                new()
                {
                    Id = 1,
                    Name = "John",
                    Email = "john@email.com",
                    PhoneNumber = "123-123-123"
                },
                new()
                {
                    Id = 2,
                    Name = "Adam",
                    Email = "adam@email.com",
                    PhoneNumber = "111-111-111"
                },
                new()
                {
                    Id = 3,
                    Name = "Anne",
                    Email = "anne@email.com",
                    PhoneNumber = "321-321-321"
                },
                new()
                {
                    Id = 4,
                    Name = "Victoria",
                    Email = "victoria@email.com",
                    PhoneNumber = "333-333-333"
                }
            });
    }
}