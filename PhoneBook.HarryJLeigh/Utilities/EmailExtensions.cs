using Phonebook.Controllers;
using Phonebook.Models;
using Spectre.Console;

namespace Phonebook.Utilities;

public static class EmailExtensions
{
    internal static Email CreateEmail(PhonebookController phonebookController)
    {
        phonebookController.ViewContacts();
        List<int> contacts = phonebookController.GetContactsId();

        int emailId = UserInputHelper.GetId(contacts, "email");
        string emailAddress = phonebookController.GetContactById(emailId).Email;

        string subject = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter email subject: ")
                .PromptStyle("cyan"));
        string body = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter email message: ")
                .PromptStyle("cyan"));

        return new Email { Address = emailAddress, Subject = subject, Body = body };
    }
}