using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Phonebook.Data.Entities;
using Phonebook.Extensions;

namespace Phonebook.Data.Contexts;

/// <summary>
/// The context for an SQL Server database.
/// </summary>
public class SqlDatabaseContext : DbContext
{
    #region Properties

    public DbSet<Contact> Contact { get; set; }

    public DbSet<Category> Category { get; set; }

    #endregion
    #region Methods

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString") ?? throw new InvalidOperationException("Database connection string not found.");

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ensure correct foreign key relationship is created.
        modelBuilder.Entity<Category>()
            .HasMany(e => e.Contacts)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId)
            .HasPrincipalKey(e => e.Id);

        // Required seed data.
        var defaultCategory = new Category { Id = 1, Name = "Default" };
        var familyCategory = new Category { Id = 2, Name = "Family" };
        var friendCategory = new Category { Id = 3, Name = "Friend" };
        var workCategory = new Category { Id = 4, Name = "Work" };

        modelBuilder.Entity<Category>().HasData(
            defaultCategory,
            familyCategory,
            friendCategory,
            workCategory
        );

        // Optional seed data.
        bool seedDatabase = ConfigurationManager.AppSettings.GetBoolean("SeedDatabase");
        if (seedDatabase)
        {
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    Id = 1,
                    Name = "John Smith",
                    Email = "john.smith@default.com",
                    PhoneNumber = "1234567890",
                    CategoryId = defaultCategory.Id,
                },
                new Contact
                {
                    Id = 2,
                    Name = "Mum",
                    Email = "mum@family.com",
                    PhoneNumber = "1234567890",
                    CategoryId = familyCategory.Id,
                },
                new Contact
                {
                    Id = 3,
                    Name = "Dad",
                    Email = "dad@family.com",
                    PhoneNumber = "1234567890",
                    CategoryId = familyCategory.Id,
                },
                new Contact
                {
                    Id = 4,
                    Name = "Bestie",
                    Email = "bestie@friend.com",
                    PhoneNumber = "1234567890",
                    CategoryId = friendCategory.Id,
                },
                new Contact
                {
                    Id = 5,
                    Name = "Boss",
                    Email = "boss@work.com",
                    PhoneNumber = "1234567890",
                    CategoryId = workCategory.Id,
                }
                );
        }
    }

    #endregion
}
