using PhoneBook.Interfaces.Menu.Command;

namespace PhoneBook.Interfaces.Menu.Factory.Initializer;

/// <summary>
/// Interface for initializing menu entries.
/// </summary>
/// <typeparam name="TEnum">The type of the enumeration representing the menu entries.</typeparam>
internal interface IMenuEntriesInitializer<TEnum>
    where TEnum : Enum
{
    /// <summary>
    /// Initializes a dictionary mapping each menu entry to its corresponding command creation function.
    /// </summary>
    /// <returns>
    /// A dictionary where the keys are entries of the menu enumeration and the values are functions
    /// that return the corresponding command instances.
    /// </returns>
    Dictionary<TEnum, Func<ICommand>> InitializeEntries();
}