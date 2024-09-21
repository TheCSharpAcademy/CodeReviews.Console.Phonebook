using PhoneBook.Interfaces.Handlers.ContactHandlers;
using PhoneBook.Interfaces.Services;
using PhoneBook.Services;
using Spectre.Console;

namespace PhoneBook.Menu.Commands.ManageMenuCommands;

internal class DeleteContactCommand : DisplayingContactsCommand
{
    private readonly IContactSelector _contactSelector;
    private readonly IContactDeleter _contactDeleter;
    
    public DeleteContactCommand(
        IContactSelector contactSelector, 
        IContactDeleter contactDeleter,
        IContactTableConstructor contactTableConstructor) : 
        base(contactTableConstructor)
    {
        _contactSelector = contactSelector;
        _contactDeleter = contactDeleter;
    }

    public override void Execute()
    {
        _contactSelector.SelectContact(out var contact, out var message);

        if (ContactIsNull(contact, message))
        {
            return;
        }
        
        DisplayContact(contact);
        
        _contactDeleter.DeleteContact(contact, out string? resultMessage);
        
        AnsiConsole.MarkupLine(resultMessage ?? DeleteCancelled);
        HelperService.PressAnyKey();
    }
}