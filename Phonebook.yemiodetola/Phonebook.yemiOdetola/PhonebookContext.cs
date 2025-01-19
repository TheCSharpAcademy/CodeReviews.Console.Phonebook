using Phonebook.yemiodetola.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

public class PhonebookContext : DbContext
{
  public DbSet<Contact> Contacts { get; set; }
  public DbSet<Category> Categories { get; set; }
  string? connectionString = ConfigurationManager.AppSettings["ConnectionString"];
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer(connectionString);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Category>().HasData(
        new Category { Id = 1, Name = "Family" },
        new Category { Id = 2, Name = "Friends" },
        new Category { Id = 3, Name = "Other" });

    modelBuilder.Entity<Contact>().HasData(
        new Contact
        {
          Id = 1,
          Name = "Yemi",
          PhoneNumber = "09137121527",
          Email = "example@gmail.com",
          CategoryId = 1
        }
    );
  }
}