using PhoneBook.Interfaces.Database;
using PhoneBook.Interfaces.Repository;
using PhoneBook.Model;

namespace PhoneBook.Repository;

/// <summary>
/// Repository for managing contact entities in the database.
/// Provides methods for adding, updating, deleting, and retrieving contacts.
/// </summary>
internal class ContactRepository : IRepository<Contact>
{
    private readonly IDatabaseManager _databaseManager;

    /// <summary>
    /// Repository for managing contact entities in the database.
    /// Provides methods for adding, updating, deleting, and retrieving contacts.
    /// </summary>
    public ContactRepository(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    /// <summary>
    /// Adds a contact to the database.
    /// </summary>
    /// <param name="entity">The contact entity to be added.</param>
    /// <returns>The number of records affected.</returns>
    public int AddContact(Contact entity)
    {
        using var connection = _databaseManager.GetConnection();
        connection.Add(entity);

        return connection.SaveChanges();
    }

    /// <summary>
    /// Updates an existing contact in the database.
    /// </summary>
    /// <param name="entity">The contact entity to update.</param>
    /// <returns>The number of state entries written to the database.</returns>
    public int UpdateContact(Contact entity)
    {
        using var connection = _databaseManager.GetConnection();
        connection.Update(entity);
        
        return connection.SaveChanges();
    }

    /// <summary>
    /// Deletes the specified contact from the repository.
    /// </summary>
    /// <param name="entity">The contact entity to be deleted.</param>
    /// <returns>The number of entities removed from the repository.</returns>
    public int DeleteContact(Contact entity)
    {
        using var connection = _databaseManager.GetConnection();
        connection.Remove(entity);
        
        return connection.SaveChanges();
    }

    /// <summary>
    /// Retrieves all contacts from the database.
    /// </summary>
    /// <returns>An array of <see cref="Contact"/> containing all contacts.</returns>
    public Contact[] GetAllContacts()
    {
        using var connection = _databaseManager.GetConnection();
        
        return connection.Contacts.ToArray();
    }
}