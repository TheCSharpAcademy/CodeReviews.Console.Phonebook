namespace Phonebook.wkktoria.Enums;

public enum MainMenuOptions
{
    [EnumExtensions.DisplayText("Manage Contacts")]
    ManageContacts,

    [EnumExtensions.DisplayText("Manage Categories")]
    ManageCategories,

    [EnumExtensions.DisplayText("Quit")] Quit
}