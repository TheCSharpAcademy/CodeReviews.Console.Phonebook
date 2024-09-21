using PhoneBook.Interfaces.Menu.Command;

namespace PhoneBook.Interfaces.Menu.Factory;

/// <summary>
/// Factory interface responsible for creating commands based on the provided menu entry.
/// </summary>
/// <typeparam name="TMenu">The type of the menu enumeration.</typeparam>
internal interface IMenuCommandsFactory<in TMenu>
{
    /// <summary>
    /// Creates an instance of an ICommand based on the specified menu entry.
    /// </summary>
    /// <param name="menuEntry">The menu entry for which the command is to be created.</param>
    /// <returns>An instance of an ICommand associated with the specified menu entry.</returns>
    /// <exception cref="InvalidOperationException">Thrown when a menu command for the specified menu entry is not found.</exception>
    ICommand Create(TMenu menuEntry);
}