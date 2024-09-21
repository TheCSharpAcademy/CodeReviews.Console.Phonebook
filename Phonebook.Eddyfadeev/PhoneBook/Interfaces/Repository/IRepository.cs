namespace PhoneBook.Interfaces.Repository;

/// <summary>
/// Interface for repository operations on contact entities.
/// </summary>
/// <typeparam name="TEntity">The type of entity being managed by the repository.</typeparam>
internal interface IRepository<TEntity>
{
    /// <summary>
    /// Adds a new contact to the repository.
    /// </summary>
    /// <param name="entity">The contact entity to be added.</param>
    /// <returns>The number of state entries written to the database.</returns>
    int AddContact(TEntity entity);

    /// <summary>
    /// Updates the details of an existing contact entity.
    /// </summary>
    /// <param name="entity">The contact entity with updated details.</param>
    /// <returns>The number of affected rows in the database.</returns>
    int UpdateContact(TEntity entity);

    /// <summary>
    /// Deletes a contact from the repository.
    /// </summary>
    /// <param name="entity">The contact to be deleted.</param>
    /// <returns>The number of state entries written to the database.</returns>
    int DeleteContact(TEntity entity);

    /// <summary>
    /// Retrieves all contacts stored in the repository.
    /// </summary>
    /// <returns>An array of all <see cref="Contact"/> instances.</returns>
    TEntity[] GetAllContacts();
}