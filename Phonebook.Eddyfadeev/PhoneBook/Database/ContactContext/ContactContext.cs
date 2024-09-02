using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneBook.Model;

namespace PhoneBook.Database.ContactContext;

/// <summary>
/// Represents the database context for managing contacts in the phone book.
/// Inherits from <see cref="DbContext"/> to provide the database configuration
/// and sets of Contact entities.
/// </summary>
internal class ContactContext : DbContext
{
    private const string DefaultConnection = "DefaultConnection";

    private readonly IConfiguration _configuration;

    public DbSet<Contact> Contacts { get; set; }

    public ContactContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString(DefaultConnection));
    }
}