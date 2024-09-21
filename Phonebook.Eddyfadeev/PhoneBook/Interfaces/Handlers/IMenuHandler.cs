namespace PhoneBook.Interfaces.Handlers;

/// <summary>
/// Provides an interface to handle menu operations within the application.
/// </summary>
public interface IMenuHandler
{
    /// <summary>
    /// Handles the display and execution of menu options based on user input.
    /// Displays the menu, waits for the user to make a selection, and executes
    /// the corresponding command.
    /// </summary>
    void HandleMenu();
}