using PhoneBook.Model;

namespace PhoneBook.Extensions;

/// <summary>
/// The ContactsExtensions class provides extension methods for the Contact class.
/// </summary>
internal static class ContactsExtensions
{
    /// <summary>
    /// Converts a <see cref="Contact"/> object into a dictionary representation.
    /// </summary>
    /// <param name="contact">The contact object to be converted.</param>
    /// <returns>A dictionary where the keys are the labels and the values are the corresponding details from the contact.</returns>
    public static Dictionary<string, string> ToDictionary(this Contact contact)
    {
        const string defaultValue = "";
        const string defaultGroup = "Unspecified";
        
        return new Dictionary<string, string>
        {
            { "Name: ", $"{contact.FirstName} {contact.LastName ?? defaultValue}" },
            { "Phone: ", $"{contact.PhoneNumber ?? defaultValue}" },
            { "Email: ", $"{contact.Email ?? defaultValue}" },
            { "Group: ", $"{contact.Group ?? defaultGroup}" }
        };
    }

    /// <summary>
    /// Extracts and concatenates the first and last name of a <see cref="Contact"/> object.
    /// </summary>
    /// <param name="contact">The contact object from which to extract the name.</param>
    /// <returns>A string containing the concatenated first and last name of the contact, separated by a space.</returns>
    public static string ExtractName(this Contact contact) =>
        $"{contact.FirstName ?? string.Empty} {contact.LastName ?? string.Empty}";
}