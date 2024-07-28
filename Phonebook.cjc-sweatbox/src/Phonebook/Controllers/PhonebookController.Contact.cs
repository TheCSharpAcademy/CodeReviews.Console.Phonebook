using Phonebook.Data.Entities;

namespace Phonebook.Controllers;

/// <summary>
/// Partial class for Contact specific model-database methods.
/// </summary>
public partial class PhonebookController
{
    #region Methods

    public bool AddContact(string name, string email, string phoneNumber, Category category)
    {
        var contact = new Contact
        {
            Name = name,
            Email = email,
            PhoneNumber = phoneNumber,
            Category = category
        };

        return _service.AddContact(contact);
    }

    public bool DeleteContact(int id)
    {
        var contact = _service.GetContact(id);
        return _service.DeleteContact(contact);
    }

    public Contact GetContact(int id)
    {
        return _service.GetContact(id);
    }

    public IReadOnlyList<Contact> GetContacts()
    {
        return _service.GetContacts();
    }

    public bool SetContact(int id, string name, string email, string phoneNumber, Category category)
    {
        var contact = _service.GetContact(id);
        contact.Name = name;
        contact.Email = email;
        contact.PhoneNumber = phoneNumber;
        contact.Category = category;

        return _service.SetContact(contact);
    }

    #endregion
}
