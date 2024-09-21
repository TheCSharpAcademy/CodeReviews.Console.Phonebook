using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PhoneBook.Database.ContactContext;

/// <summary>
/// Factory for creating instances of <see cref="ContactContext"/> at design time.
/// Implements <see cref="IDesignTimeDbContextFactory{ContactContext}"/> to facilitate
/// the creation of the context for use with tools like Entity Framework Core migrations.
/// </summary>
internal class ContactContextFactory : IDesignTimeDbContextFactory<ContactContext>
{
    private const string DefaultConnection = "DefaultConnection";
    public ContactContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        
        var optionsBuilder = new DbContextOptionsBuilder<ContactContext>();
        string? connectionString = configuration.GetConnectionString(DefaultConnection);
        optionsBuilder.UseSqlServer(connectionString);

        return new ContactContext(configuration);
    }
}