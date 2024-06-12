
using Microsoft.EntityFrameworkCore;
using Phonebook.Entities;

namespace Phonebook.Data;

public class PhonebookDbContext : DbContext
{
    string DatabaseUserID;
    string DatabasePassword;

    public PhonebookDbContext(string databaseUserID, string databasePassword)
    {
        DatabaseUserID = databaseUserID;
        DatabasePassword = databasePassword;
    }

    
    public DbSet<Contact> Contacts { get; set; }

    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        optionsBuilder.UseSqlServer(@$"Server=localhost,1433;Initial Catalog=Phonebook; User Id={DatabaseUserID}; Password={DatabasePassword};TrustServerCertificate=True");
    }
}