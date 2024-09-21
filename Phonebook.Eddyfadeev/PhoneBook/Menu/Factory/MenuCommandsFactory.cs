using PhoneBook.Interfaces.Menu.Command;
using PhoneBook.Interfaces.Menu.Factory;
using PhoneBook.Interfaces.Menu.Factory.Initializer;

namespace PhoneBook.Menu.Factory;

internal class MenuCommandsFactory<TMenu> : IMenuCommandsFactory<TMenu> 
    where TMenu : struct, Enum
{
    private readonly Dictionary<TMenu, Func<ICommand>> _menuCommands;
    
    public MenuCommandsFactory(IMenuEntriesInitializer<TMenu> menuEntriesInitializer)
    {
        _menuCommands = menuEntriesInitializer.InitializeEntries();
    }
    
    public ICommand Create(TMenu menuEntry)
    {
        if (_menuCommands.TryGetValue(menuEntry, out var menuCommand))
        {
            return menuCommand();
        }
        
        throw new InvalidOperationException($"Menu command not found for the { menuCommand }.");
    }
}