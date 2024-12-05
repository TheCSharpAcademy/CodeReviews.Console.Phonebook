using Microsoft.EntityFrameworkCore;
using System.Configuration;
using PhoneBook.Models;

namespace PhoneBook.Data;

internal class PhoneBookContext : DbContext
{
    public DbSet<Contact>? Contacts { get; set; }

    private readonly string? _connectionString;

    public PhoneBookContext()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => base.OnConfiguring(optionsBuilder.UseSqlServer(_connectionString));
}