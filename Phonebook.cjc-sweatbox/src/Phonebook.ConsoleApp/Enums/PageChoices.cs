using System.ComponentModel;

namespace Phonebook.ConsoleApp.Enums;

/// <summary>
/// Supported choices for all pages in the application.
/// </summary>
internal enum PageChoices
{
    [Description("Default")]
    Default,
    [Description("Close application")]
    CloseApplication,
    [Description("Close page")]
    ClosePage,
    [Description("Create contact")]
    CreateContact,
    [Description("Remove contact")]
    RemoveContact,
    [Description("Update contact")]
    UpdateContact,
    [Description("View contacts")]
    ViewContacts
}

