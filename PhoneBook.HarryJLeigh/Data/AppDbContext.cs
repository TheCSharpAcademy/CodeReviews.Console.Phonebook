using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.Data;

public class AppDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DatabasePath"].ConnectionString;
        optionsBuilder.UseSqlServer(connectionString);
    }
}