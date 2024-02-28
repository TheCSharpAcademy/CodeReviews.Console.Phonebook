using Phonebook.Controllers;
using Phonebook.Models;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace Phonebook.Services;
internal class EmailServices
{

    internal static Contact AddEmail(Contact contact)
    {
        Email newEmail = new Email();
        bool emailValid = false;
        bool duplicate = true;
        while (!emailValid || duplicate)
        {
            newEmail.EmailAddress = AnsiConsole.Ask<string>("Email Address:");
            emailValid = Validators.IsValidEmail(newEmail.EmailAddress);
            duplicate = Validators.Duplicate(newEmail.EmailAddress, contact.Emails);

            if (!emailValid)
            {
                Console.WriteLine($"{newEmail.EmailAddress} is an invalid entry");
            }
            if (duplicate)
            {
                Console.WriteLine($"{newEmail.EmailAddress} is already entered for {contact.FirstName} {contact.LastName} ");
            }
            if (!emailValid || duplicate)
            {
                Console.WriteLine("Press enter to continue. Press X to Go Back.");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (char.ToUpper(keyInfo.KeyChar) == 'X')
                {
                    return contact;
                }
            }

        }
        newEmail.ContactId = contact.ContactId;
        newEmail = EmailController.AddEmail(newEmail);
        contact.Emails.Add(newEmail);
        return contact;
    }

    internal static Email GetEmailOptionInput(Contact contact)
    {
        var emailArray = contact.Emails
            .Select(x => $"{x.EmailAddress}").ToArray();

        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose Email")
            .AddChoices(emailArray));

        var selectedEmail = contact.Emails.FirstOrDefault(x => $"{x.EmailAddress}" == option);
        return selectedEmail;
    }

    internal static void DeleteEmail(Email emailToDelete)
    {
        bool delete = false;
        delete = AnsiConsole.Confirm($"Delete Email: {emailToDelete.EmailAddress}");

        if (delete)
        {
            EmailController.RemoveEmail(emailToDelete);
        }

    }

    internal static Contact RemoveEmailFromContact(Email emailToDelete, Contact contact)
    {
        var emailToRemove = contact.Emails.FirstOrDefault(e => e.EmailId == emailToDelete.EmailId);

        if (emailToRemove != null) 
        {
            contact.Emails.Remove(emailToRemove);
        }

        return contact;
    }
    public static string RemoveHtmlTags(string input)
    {
        return Regex.Replace(input, "<.*?>", string.Empty);
    }

}
