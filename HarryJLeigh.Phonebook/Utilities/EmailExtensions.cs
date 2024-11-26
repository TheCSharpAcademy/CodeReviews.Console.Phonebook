using Phonebook.Models;
using Phonebook.Services;
using Spectre.Console;

namespace Phonebook.Utilities;

public static class EmailExtensions
{
    internal static Email CreateEmail()
    {
        PhonebookService.ViewContacts();
        List<int> contacts = PhonebookService.GetContactsId();

        int emailId = UserInputHelper.GetId(contacts, "email");
        string emailAddress = PhonebookService.GetContactById(emailId)[0].Email;

        string subject = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter email subject: ")
                .PromptStyle("cyan"));
        string body = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter email message: ")
                .PromptStyle("cyan"));

        return new Email { Address = emailAddress, Subject = subject, Body = body };
    }
}