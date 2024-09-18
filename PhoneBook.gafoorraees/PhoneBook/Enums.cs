using System.Runtime.CompilerServices;

namespace PhoneBook;

internal static class Enums
{
    internal enum MainMenuOptions
    {
        AddContact,
        DeleteContact,
        UpdateContact,
        ViewContacts,
        Quit,
    }

    public static string MainMenuDisplayOptions(this MainMenuOptions option)
    {
        return option switch
        {
            MainMenuOptions.AddContact => "Add Contact",
            MainMenuOptions.DeleteContact => "Delete Contact",
            MainMenuOptions.UpdateContact => "Update Contact",
            MainMenuOptions.ViewContacts => "View Contacts",
            MainMenuOptions.Quit => "Quit",
            _ => option.ToString()
        };
    }
}
