namespace Console.Phonebook;

internal enum MainMenuOptions
{
    CurrentContacts,
    AddContact,
    ManageCategories,
    SendMessage,
    Quit
}

internal enum ContactMenuOptions
{
    ViewFull,
    Edit,
    Delete,
    Back
}

internal enum EditMenuOptions
{
    Name,
    Email,
    PhoneNumber,
    Category,
    Back
}

internal enum SendMessageOptions
{
    Email,
    SMS,
    Back
}

internal enum ManageCategoriesOptions
{
    Add,
    Delete,
    Back
}
