using PhoneBook.Model;

namespace PhoneBook.Interfaces.Handlers;

/// <summary>
/// Interface defining a contract for sending emails to contacts.
/// </summary>
internal interface IEmailSender
{
    /// <summary>
    /// Sends an email to the specified contact.
    /// </summary>
    /// <param name="contact">The contact to whom the email will be sent.</param>
    void SendEmail(Contact contact);
}