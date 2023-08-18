using System.Globalization;
using System.Text.RegularExpressions;
using Spectre.Console;

using Phonebook.MartinL_no.Models;
using Phonebook.MartinL_no.Controllers;
using Phonebook.MartinL_no.UserInterfaces;

namespace Phonebook.MartinL_no.Services;

internal static class ContactService
{
    public static void AddContact()
	{
        var name = AnsiConsole.Ask<string>("Contact's name: ");
        var phoneNumber = AnsiConsole.Ask<string>("Phone number: ");
        var email = "";
        while (!IsValidEmail(email))
        {
            email = AnsiConsole.Ask<string>("Email address: ");
        }
        var type = GetContactType();

        ContactController.AddContact(new Contact { Name = name, PhoneNumber = phoneNumber, Email = email, Type = type });
    }

    public static void DeleteContact()
	{
		var contact = GetContactOptionInput();
		ContactController.DeleteContact(contact);
	}

    public static void GetContacts()
    {
        var contacts = ContactController.GetContacts();
        UserInterface.ShowContacts(contacts);
    }

    public static void GetContact()
    {
        var contact = GetContactOptionInput();
		UserInterface.ShowContact(contact);
    }

    public static void GetContactsByType()
    {
        var category = GetContactType();
        var contacts = ContactController.GetContacts().Where(x => x.Type == category).ToList();
        UserInterface.ShowContacts(contacts);
    }

    public static void UpdateContact()
    {
        var contact = GetContactOptionInput();
        contact.Name = AnsiConsole.Ask<string>("Contact's new name: ");
        contact.PhoneNumber = AnsiConsole.Ask<string>("Contact's new phone number: ");

        var email = "";
        while (!IsValidEmail(email))
        {
            email = AnsiConsole.Ask<string>("New email address: ");
        }

        contact.Type = GetContactType();

        ContactController.UpdateContact(contact);
    }

    public static async Task SendEmail()
    {
        var contact = GetContactOptionInput();
        var subject = AnsiConsole.Ask<string>("Subject: ");
        var content = AnsiConsole.Ask<string>("Content: ");

        await EmailController.SendEmail(contact, subject, content);
    }

    private static Contact GetContactOptionInput()
	{
		var contacts = ContactController.GetContacts();
		var contactsArray = contacts.Select(x => x.Name).ToArray();
		var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
			.Title("Choose Contact")
			.AddChoices(contactsArray));
		var id = contacts.Single(x => x.Name == option).Id;
		var contact = ContactController.GetContactById(id);

		return contact;
	}

    private static ContactType GetContactType()
    {
        return AnsiConsole.Prompt(
        new SelectionPrompt<ContactType>()
        .Title("Choose contact type:")
        .AddChoices(
            ContactType.Family,
            ContactType.Friends,
            ContactType.Work,
            ContactType.None));
    }

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
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
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
}