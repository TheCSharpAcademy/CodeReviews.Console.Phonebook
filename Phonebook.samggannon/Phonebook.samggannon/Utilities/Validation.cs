using Phonebook.samggannon.Models;
using Phonebook.samggannon.Services;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Phonebook.samggannon.Utilities;

internal static class Validation
{
    public static bool IsEmailValid(string emailAddress)
    {

        if (string.IsNullOrEmpty(emailAddress))
            return false;

        try
        {
            var mailAddress = new MailAddress(emailAddress);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
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
