namespace Phonebook.Models;


internal class Enums
{
    internal enum MainMenuOptions
    {
        Messaging,
        Sent,
        ManageContacts,
        Settings,
        Quit
    }

    internal enum ManageContactsMenuOptions
    {
        AddContact,
        ManageContactMethods,
        UpdateContact,
        DeleteContact,
        Back
    }

    internal enum ManageContactMethodsMenu
    {
        ManageEmails,
        ManagePhoneNumbers,
        Back
    }

    internal enum EmailMenuOptions
    {
        AddEmail,
        RemoveEmail,
        Back
    }

    internal enum PhoneMenuOptions
    {
        AddPhoneNumber,
        RemovePhoneNumber,
        Back
    }

    internal enum MessagingOptions
    {
        Email,
        SMS,
        Back
    }

    internal enum SendMenu
    {
        Send,
        Edit,
        Discard
    }

    internal enum EmailHistoryMenu
    {
        ViewSingleEmail,
        Back,
    }

    internal enum SmsHistoryMenu
    {
        ViewSingleText,
        Back,
    }

    internal enum SettingsMenuOptions
    {
        ModifySettings,
        RemoveAllSettings,
        Back
    }
}


