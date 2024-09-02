using PhoneBook.Database.ContactContext;

namespace PhoneBook.Interfaces.Database;

/// <summary>
/// Provides methods to manage and retrieve a database connection.
/// </summary>
internal interface IDatabaseManager
{
    /// <summary>
    /// Creates and returns a new instance of the ContactContext using the configured database connection settings.
    /// </summary>
    /// <returns>A new instance of ContactContext representing the database connection.</returns>
    ContactContext GetConnection();
}