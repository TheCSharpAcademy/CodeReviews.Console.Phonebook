using PhoneBook.Enums;
using PhoneBook.Extensions;
using PhoneBook.Interfaces.Handlers.ContactHandlers;
using PhoneBook.Interfaces.Menu;
using PhoneBook.Interfaces.Repository;
using PhoneBook.Model;
using PhoneBook.Services;
using Spectre.Console;

namespace PhoneBook.Handlers.ContactHandlers;

/// <summary>
/// ContactUpdater is responsible for updating a contact in the repository.
/// </summary>
internal class ContactUpdater : IContactUpdater
{
    private readonly IRepository<Contact> _contactRepository;
    private readonly IMenuEntries _menuEntries;

    public ContactUpdater(IRepository<Contact> contactRepository, IMenuEntries menuEntries)
    {
        _contactRepository = contactRepository;
        _menuEntries = menuEntries;
    }

    /// <summary>
    /// Updates the specified contact and returns a status message indicating the result of the operation.
    /// </summary>
    /// <param name="contact">The contact to be updated.</param>
    /// <param name="message">An output parameter that holds the result message of the update operation.</param>
    public void UpdateContact(Contact contact, out string? message)
    {
        bool update = PromptService.ConfirmAction(UpdatePrompt);

        if (!update)
        {
            message = default;
            return;
        }

        var requestedChanges = 
            AnsiConsole.Prompt(GetContactEditPrompt())
                .Select(EnumExtensions.GetEnumValueFromDescription<ContactEditOptions>)
                .ToArray();

        PromptService.PromptStrategyResolver(contact, requestedChanges);
        
        var result = _contactRepository.UpdateContact(contact);

        message = result switch
        {
            > 0 => ContactUpdated,
            0 => ContactWasNotUpdated,
            _ => default
        };
    }
    
    private MultiSelectionPrompt<string> GetContactEditPrompt()
    {
        const string message = "Please select what would you like to edit.\n" +
                               "Do not choose anything and press enter, if do not want to edit the contact.";

        var prompt = _menuEntries.GetSelectableMenuEntries<ContactEditOptions>(message);
        
        return prompt;
    }
}