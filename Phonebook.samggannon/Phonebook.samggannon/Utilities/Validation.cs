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
            // Normalize the domain
            emailAddress = Regex.Replace(emailAddress, @"(@)(.+)$", DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            return false;
        }
        catch (ArgumentException e)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(emailAddress,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
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
