using Microsoft.EntityFrameworkCore;

namespace Phonebook.kjanos89;

public class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PhonebookDB"]?.ConnectionString;
        optionsBuilder.UseSqlServer(connectionString);
    }
}
