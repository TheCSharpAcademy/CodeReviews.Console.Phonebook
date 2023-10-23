using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Phonebook.wkktoria.Models;

namespace Phonebook.wkktoria;

public class AppDbContext : DbContext
{
    public DbSet<Contact>? Contacts { get; set; }
    public DbSet<Category>? Categories { get; set; }

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
    }
}