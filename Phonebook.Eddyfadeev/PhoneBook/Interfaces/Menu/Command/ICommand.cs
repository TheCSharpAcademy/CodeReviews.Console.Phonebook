namespace PhoneBook.Interfaces.Menu.Command;

/// <summary>
/// Represents a command that can be executed.
/// </summary>
internal interface ICommand
{
    /// <summary>
    /// Executes an action defined by the command.
    /// </summary>
    internal void Execute();
}