using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneBook.StevieTV.Models;

namespace PhoneBook.StevieTV.Database;

public class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    private static IConfiguration _config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();
    
    private readonly string _connectionString = _config.GetSection("Settings")["ConnectionString"];

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}