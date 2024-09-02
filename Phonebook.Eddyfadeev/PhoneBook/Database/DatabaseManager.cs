using Microsoft.Extensions.Configuration;
using PhoneBook.Interfaces.Database;

namespace PhoneBook.Database;

/// <summary>
/// Manages the database connection for the application by initializing
/// and providing access to the ContactContext.
/// </summary>
internal class DatabaseManager : IDatabaseManager
{
    private readonly IConfiguration _connectionConfiguration;
    
    public DatabaseManager(IConfiguration configuration)
    {
        _connectionConfiguration = configuration;
        InitializeDatabase();
    }

    /// <summary>
    /// Creates and returns a new instance of the ContactContext using the configured database connection settings.
    /// </summary>
    /// <returns>A new instance of ContactContext representing the database connection.</returns>
    public ContactContext.ContactContext GetConnection() =>
        new (_connectionConfiguration);
    
    private void InitializeDatabase()
    {
        using var connection = GetConnection();
        
        connection.Database.EnsureCreated();
    }
}