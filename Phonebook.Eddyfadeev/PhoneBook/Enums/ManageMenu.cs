namespace PhoneBook.Enums;

/// <summary>
/// Defines the options for managing contacts within the phone book.
/// The available options include adding a new contact, editing an existing contact,
/// deleting a contact, and returning to the main menu.
/// </summary>
[Description("Manage Phone Book")]
internal enum ManageMenu
{
    [Description("Add a new contact")]
    Add,
    [Description("Edit a contact")]
    Edit,
    [Description("Delete a contact")]
    Delete,
    [Description("Back")]
    Back
}