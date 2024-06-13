
using Microsoft.EntityFrameworkCore;
using Phonebook.Entities;

namespace Phonebook.Data;

public class PhonebookDbContext : DbContext
{
    string DatabaseUserID;
    string DatabasePassword;

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ContactCategory> ContactCategories{ get; set; }
    
    public PhonebookDbContext(string databaseUserID, string databasePassword)
    {
        DatabaseUserID = databaseUserID;
        DatabasePassword = databasePassword;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ContactCategory>()
            .HasIndex(c => c.CategoryName)
            .IsUnique();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        optionsBuilder.UseSqlServer(@$"Server=localhost,1433;Initial Catalog=Phonebook; User Id={DatabaseUserID}; Password={DatabasePassword};TrustServerCertificate=True");
    }
}