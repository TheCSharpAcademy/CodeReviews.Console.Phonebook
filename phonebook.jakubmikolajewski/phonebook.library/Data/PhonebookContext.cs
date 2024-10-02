using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Phonebook.Library.Models;

namespace Phonebook.Library.Data;

public class PhonebookContext : DbContext
{
    private readonly string _connectionString;

    public PhonebookContext()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    public DbSet<Contact> Contacts { get; set; }
}
