using PhoneBook.Enums;
using PhoneBook.Exceptions;
using PhoneBook.Interfaces.Handlers.ContactHandlers;
using PhoneBook.Interfaces.Menu.Command;
using PhoneBook.Interfaces.Menu.Factory.Initializer;
using PhoneBook.Interfaces.Services;
using PhoneBook.Menu.Commands.ManageMenuCommands;

namespace PhoneBook.Menu.Factory.Initializers;

internal sealed class ManageMenuEntries : IMenuEntriesInitializer<ManageMenu>
{
    private readonly IContactTableConstructor _contactTableConstructor;
    private readonly IContactSelector _contactSelector;
    private readonly IContactAdder _contactAdder;
    private readonly IContactUpdater _contactUpdater;
    private readonly IContactDeleter _contactDeleter;
    
    public ManageMenuEntries(
        IContactTableConstructor contactTableConstructor, 
        IContactSelector contactSelector,
        IContactAdder contactAdder,
        IContactUpdater contactUpdater,
        IContactDeleter contactDeleter
        )
    {
        _contactTableConstructor = contactTableConstructor;
        _contactSelector = contactSelector;
        _contactAdder = contactAdder;
        _contactUpdater = contactUpdater;
        _contactDeleter = contactDeleter;
    }
    
    public Dictionary<ManageMenu, Func<ICommand>> InitializeEntries() =>
        new()
        {
            { ManageMenu.Add, () => new AddContactCommand(_contactAdder) },
            { ManageMenu.Edit, () => new EditContactCommand(_contactSelector, _contactUpdater, _contactTableConstructor) },
            { ManageMenu.Delete, () => new DeleteContactCommand(_contactSelector, _contactDeleter, _contactTableConstructor) },
            { ManageMenu.Back, () => throw new ReturnToMainMenuException() }
        };
}