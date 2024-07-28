namespace Phonebook.ConsoleApp.Models;

/// <summary>
/// Used to display a choice that requires a backing ID value.
/// </summary>
internal class PageChoice
{
    #region Properties

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    #endregion
}
