using Phonebook.ConsoleApp.Services;
using Phonebook.Data.Entities;
using Phonebook.Models;

namespace Phonebook.ConsoleApp.Views;

/// <summary>
/// Page which allows users to create a contact.
/// </summary>
internal class CreateContactPage : BasePage
{
    #region Constants

    private const string PageTitle = "Create Contact";

    #endregion
    #region Methods - Internal

    internal static CreateContactRequest? Show(IReadOnlyList<Category> categories)
    {
        WriteHeader(PageTitle);

        var name = UserInputService.GetString($"Enter the contact's [blue]name[/], or [blue]0[/] to cancel: ");
        if (name == "0")
        {
            return null;
        }

        var email = UserInputService.GetEmailAddress($"Enter the contact's [blue]email address[/], or [blue]0[/] to cancel: ");
        if (email == "0")
        {
            return null;
        }

        var phone = UserInputService.GetPhoneNumber($"Enter the contact's [blue]phone number[/], or [blue]0[/] to cancel: ");
        if (phone == "0")
        {
            return null;
        }

        var category = UserInputService.GetCategory($"Select a [blue]category[/] for the contact: ", categories);
        if (category is null)
        {
            return null;
        }

        return new CreateContactRequest
        {
            Name = name,
            Email = email,
            PhoneNumber = phone,
            Category = category
        };
    }

    #endregion
}
