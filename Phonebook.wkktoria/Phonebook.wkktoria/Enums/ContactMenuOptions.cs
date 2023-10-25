namespace Phonebook.wkktoria.Enums;

public enum ContactMenuOptions
{
    [EnumExtensions.DisplayText("Add Contact")]
    AddContact,

    [EnumExtensions.DisplayText("Update Contact")]
    UpdateContact,

    [EnumExtensions.DisplayText("Delete Contact")]
    DeleteContact,

    [EnumExtensions.DisplayText("View Contact Details")]
    ViewContactDetails,

    [EnumExtensions.DisplayText("View Contacts")]
    ViewContacts,

    [EnumExtensions.DisplayText("Go Back")]
    GoBack
}