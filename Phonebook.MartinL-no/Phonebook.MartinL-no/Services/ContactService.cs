using Spectre.Console;

using Phonebook.MartinL_no.Models;
using Phonebook.MartinL_no.Controllers;
using Phonebook.MartinL_no.UserInterfaces;

namespace Phonebook.MartinL_no.Services;

internal static class ContactService
{
    public static void AddContact()
    {
        var name = GetNameInput();
        var phoneNumber = GetPhoneNumberInput();
        var email = GetEmailInput();

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

        contact.Name = GetNameInput();
        contact.PhoneNumber = GetPhoneNumberInput();
        contact.Email = GetEmailInput();
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

    private static string GetNameInput()
    {
        var contacts = ContactController.GetContacts();
        while (true)
        {
            var name = AnsiConsole.Ask<string>("Name: ");

            if (!contacts.Exists(x => x.Name.ToLower() == name.ToLower())) return name;
            else ShowMessage("Contact already exist, please choose another name");
        }
    }

    private static string GetPhoneNumberInput()
    {
        while (true)
        {
            var phoneNumber = AnsiConsole.Ask<string>("Phone Number (format must be +4795634657): ");

            if (Validation.IsValidPhoneNumber(phoneNumber)) return phoneNumber;
            else ShowMessage("Invalid phone number, please try again");
        }
    }

    private static string GetEmailInput()
    {
        while (true)
        {
            var email = AnsiConsole.Ask<string>("Email address: ");

            if (Validation.IsValidEmail(email)) return email;
            else ShowMessage("Invalid email address, please try again");
        }
    }

    private static void ShowMessage(string message)
    {
        Console.WriteLine(message);
        Thread.Sleep(2000);
        Console.Clear();
    }
}