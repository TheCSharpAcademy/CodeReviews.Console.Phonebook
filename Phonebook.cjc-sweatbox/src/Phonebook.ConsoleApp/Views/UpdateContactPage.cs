using Phonebook.ConsoleApp.Engines;
using Phonebook.ConsoleApp.Services;
using Phonebook.Data.Entities;
using Phonebook.Models;
using Spectre.Console;

namespace Phonebook.ConsoleApp.Views;

/// <summary>
/// Page which allows users to update a contact.
/// </summary>
internal class UpdateContactPage : BasePage
{
    #region Constants

    private const string PageTitle = "Update Contact";

    #endregion
    #region Methods - Internal

    internal static UpdateContactRequest? Show(Contact contact, IReadOnlyList<Category> categories)
    {
        WriteHeader(PageTitle);

        // Show user the what is being updated.
        var table = TableEngine.GetContactTable(contact);
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();

        var name = UserInputService.GetString($"Enter the contact's [blue]name[/], [blue]enter key[/] to keep existing, or [blue]0[/] to cancel: ", contact.Name);
        if (name == "0")
        {
            return null;
        }

        var email = UserInputService.GetEmailAddress($"Enter the contact's [blue]email address[/], [blue]enter key[/] to keep existing, or [blue]0[/] to cancel: ", contact.Email);
        if (email == "0")
        {
            return null;
        }

        var phone = UserInputService.GetPhoneNumber($"Enter the contact's [blue]phone number[/], [blue]enter key[/] to keep existing, or [blue]0[/] to cancel: ", contact.PhoneNumber);
        if (phone == "0")
        {
            return null;
        }

        var category = UserInputService.GetCategory($"Select a [blue]category[/] for the contact: ", categories);
        if (category is null)
        {
            return null;
        }

        return new UpdateContactRequest
        {
            Id = contact.Id,
            Name = name,
            Email = email,
            PhoneNumber = phone,
            Category = category
        };
    }

    #endregion
}
