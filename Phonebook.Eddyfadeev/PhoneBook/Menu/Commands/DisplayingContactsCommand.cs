using PhoneBook.Interfaces.Menu.Command;
using PhoneBook.Interfaces.Services;
using PhoneBook.Model;
using PhoneBook.Services;
using Spectre.Console;

namespace PhoneBook.Menu.Commands;

internal abstract class DisplayingContactsCommand : ICommand
{
    private readonly IContactTableConstructor _contactTableConstructor;

    protected DisplayingContactsCommand(IContactTableConstructor contactTableConstructor)
    {
        _contactTableConstructor = contactTableConstructor;
    }

    public abstract void Execute();
    
    private protected void DisplayContact(Contact contact)
    {
        var contactTable = _contactTableConstructor.CreateContactTable(contact);
        
        AnsiConsole.Write(contactTable);
    }

    private protected static bool ContactIsNull(Contact? contact, string message)
    {
        if (contact is null)
        {
            AnsiConsole.MarkupLine(message);
            HelperService.PressAnyKey();
            return true;
        }

        return false;
    }
}