using PhoneBook.Interfaces.Handlers.ContactHandlers;
using PhoneBook.Interfaces.Services;
using PhoneBook.Services;
using Spectre.Console;

namespace PhoneBook.Menu.Commands.ManageMenuCommands;

internal class EditContactCommand : DisplayingContactsCommand
{
    private readonly IContactSelector _contactSelector;
    private readonly IContactUpdater _contactUpdater;
    
    public EditContactCommand(
        IContactSelector contactSelector, 
        IContactUpdater contactUpdater,
        IContactTableConstructor contactTableConstructor
    ) : base(contactTableConstructor)
    {
        _contactSelector = contactSelector;
        _contactUpdater = contactUpdater;
    }

    public override void Execute()
    {
        _contactSelector.SelectContact(out var contact, out var message);

        if (ContactIsNull(contact, message))
        {
            return;
        }
        
        DisplayContact(contact);

        _contactUpdater.UpdateContact(contact, out var updateMessage);
        
        AnsiConsole.MarkupLine(updateMessage ?? UpdateCancelled);
        HelperService.PressAnyKey();
    }
}