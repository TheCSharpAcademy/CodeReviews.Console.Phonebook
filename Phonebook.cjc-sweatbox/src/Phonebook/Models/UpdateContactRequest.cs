using Phonebook.Data.Entities;

namespace Phonebook.Models;

/// <summary>
/// Class to hold required values for updating a contact.
/// </summary>
public class UpdateContactRequest
{
    #region Properties

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public Category Category { get; set; } = new Category();

    #endregion
}
