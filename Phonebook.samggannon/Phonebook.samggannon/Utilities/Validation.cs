using Phonebook.samggannon.Services;
using Phonebook.samggannon.Models;

namespace Phonebook.samggannon.Utilities;

internal static class Validation
{
    public static bool IsEmailValid(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        return email.Contains("@");
    }

    public static bool IsPhoneNumberValid(string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
            return false;

        return phoneNumber.Length >= 10 && phoneNumber.All(char.IsDigit);
    }

    public static void SendEmailIfProvided(Contact contact)
    {
        if (!string.IsNullOrEmpty(contact.Email))
        {
            EmailSender sender = new EmailSender();
            sender.SendMail(contact.Email, contact.Name);
        }
    }
}
