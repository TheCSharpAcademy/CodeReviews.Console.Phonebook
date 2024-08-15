using Microsoft.EntityFrameworkCore;
using jollejonas.Phonebook.Models;
using Microsoft.Extensions.Logging;


namespace jollejonas.Phonebook.Data;

public class PhonebookContext : DbContext
{
    public PhonebookContext(DbContextOptions<PhonebookContext> options) : base(options)
    {
    }


    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data for Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Friends" },
            new Category { Id = 2, Name = "Family" },
            new Category { Id = 3, Name = "Work" }
        );
    }
}


