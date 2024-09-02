using System.ComponentModel;

namespace PhoneBook.Enums;

/// <summary>
/// Enum representing the main menu options in the PhoneBook application.
/// </summary>
[Description("Main Menu")]
internal enum MainMenu
{
    [Description("View all contacts")]
    ViewAllContacts,
    [Description("Manage contacts")]
    ManageContacts,
    [Description("Exit")]
    Exit
}