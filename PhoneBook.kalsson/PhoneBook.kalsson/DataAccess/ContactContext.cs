using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneBook.kalsson.Models;

namespace PhoneBook.kalsson.DataAccess;

public class ContactContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Email> EmailAddresses { get; set; }
    public DbSet<Phone> PhoneNumbers { get; set; }
    
    /// <summary>
    /// This method is called by the framework when configuring the context to be used for database operations.
    /// It sets the connection string for the database based on the configuration in the appsettings.json file.
    /// </summary>
    /// <param name="options">The options for configuring the context.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Initialize a new instance of ConfigurationBuilder class.
        var builder = new ConfigurationBuilder()
            // Set the base path for the configuration builder to the current directory.
            .SetBasePath(Directory.GetCurrentDirectory())
            // Add a new configuration source for a JSON configuration file named "appsettings.json".
            .AddJsonFile("appsettings.json");

        // Build the configuration and retrieve the resulting configuration tree.
        var config = builder.Build();

        // Configure the database to use a SQL Server database 
        // The connection string is retrieved from the configuration built earlier under the key "Default".
        options.UseSqlServer(config.GetConnectionString("Default"));
    }
}