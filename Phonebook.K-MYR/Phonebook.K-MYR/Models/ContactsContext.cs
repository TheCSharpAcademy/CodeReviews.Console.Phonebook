using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Phonebook.K_MYR.Models;

internal class ContactsContext : DbContext
{
    private readonly string _connectionString = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder
    {
        DataSource = ConfigurationManager.AppSettings.Get("DataSource"),
        InitialCatalog = ConfigurationManager.AppSettings.Get("DatabaseName"),
        UserID = ConfigurationManager.AppSettings.Get("UserName"),
        Password = ConfigurationManager.AppSettings.Get("Password"),
        TrustServerCertificate = true
    }.ConnectionString;

    public DbSet<Contact> Contacts { get; set; }

    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .HasOne(con => con.Category)
            .WithMany(cat => cat.Contacts)
            .HasForeignKey(cat => cat.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.Entity<Category>()
            .HasData(new Category { CategoryId = 1, Name = "General" });
    }
}
