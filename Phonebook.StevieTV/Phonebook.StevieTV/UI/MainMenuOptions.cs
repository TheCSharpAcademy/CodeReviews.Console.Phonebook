using System.ComponentModel;

namespace Phonebook.StevieTV.UI;

public enum MainMenuOptions
{
    [Description("View Contacts")]
    ViewContacts,
    [Description("Add a Contact")]
    AddContact,
    [Description("Delete a Contact")]
    DeleteContact,
    [Description("Update a Contact")]
    UpdateContact,
    [Description("Exit")]
    Exit
}