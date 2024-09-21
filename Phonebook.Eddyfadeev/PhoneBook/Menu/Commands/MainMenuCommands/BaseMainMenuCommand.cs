using PhoneBook.Interfaces.Handlers;
using PhoneBook.Interfaces.Menu.Command;

namespace PhoneBook.Menu.Commands.MainMenuCommands;

public abstract class BaseMainMenuCommand : ICommand
{
    private readonly IMenuHandler _menuHandler;
    protected BaseMainMenuCommand(IMenuHandler menuHandler)
    {
        _menuHandler = menuHandler;
    }

    public virtual void Execute()
    {
        _menuHandler.HandleMenu();
    }
}