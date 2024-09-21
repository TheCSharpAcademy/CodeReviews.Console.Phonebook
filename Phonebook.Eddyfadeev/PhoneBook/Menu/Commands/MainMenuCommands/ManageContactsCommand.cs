using PhoneBook.Enums;
using PhoneBook.Handlers;

namespace PhoneBook.Menu.Commands.MainMenuCommands;

internal sealed class ManageContactsCommand : BaseMainMenuCommand
{
    public ManageContactsCommand(MenuHandler<ManageMenu> menuHandler) : base(menuHandler)
    {
    }
}