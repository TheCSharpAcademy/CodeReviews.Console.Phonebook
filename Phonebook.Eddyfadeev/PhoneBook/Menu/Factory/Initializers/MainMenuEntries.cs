using PhoneBook.Enums;
using PhoneBook.Exceptions;
using PhoneBook.Handlers;
using PhoneBook.Interfaces.Handlers;
using PhoneBook.Interfaces.Handlers.ContactHandlers;
using PhoneBook.Interfaces.Menu.Command;
using PhoneBook.Interfaces.Menu.Factory.Initializer;
using PhoneBook.Interfaces.Services;
using PhoneBook.Menu.Commands.MainMenuCommands;

namespace PhoneBook.Menu.Factory.Initializers;

internal sealed class MainMenuEntries : IMenuEntriesInitializer<MainMenu>
{
    private readonly MenuHandler<ManageMenu> _manageMenuHandler;
    private readonly IContactSelector _contactSelector;
    private readonly IContactTableConstructor _contactTableConstructor;
    private readonly IEmailSender _emailSender;

    public MainMenuEntries(
        MenuHandler<ManageMenu> manageMenuHandler,
        IContactSelector contactSelector,
        IContactTableConstructor contactTableConstructor,
        IEmailSender emailSender
        )
    {
        _manageMenuHandler = manageMenuHandler;
        _contactSelector = contactSelector;
        _contactTableConstructor = contactTableConstructor;
        _emailSender = emailSender;
    }
    
    public Dictionary<MainMenu, Func<ICommand>> InitializeEntries() => 
        new()
        {
            { MainMenu.ViewAllContacts, () => new ViewAllContactsCommand(_contactSelector, _contactTableConstructor, _emailSender) },
            { MainMenu.ManageContacts, () => new ManageContactsCommand(_manageMenuHandler) },
            { MainMenu.Exit, () => throw new ExitApplicationException()}
        };
}