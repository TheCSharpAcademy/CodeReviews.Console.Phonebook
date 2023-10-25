namespace Phonebook.wkktoria.Enums;

public enum CategoryMenuOptions
{
    [EnumExtensions.DisplayText("Add Category")]
    AddCategory,

    [EnumExtensions.DisplayText("Update Category")]
    UpdateCategory,

    [EnumExtensions.DisplayText("Delete Category")]
    DeleteCategory,

    [EnumExtensions.DisplayText("View Contacts In Category")]
    ViewContactsInCategory,

    [EnumExtensions.DisplayText("View Categories")]
    ViewCategories,

    [EnumExtensions.DisplayText("Go Back")]
    GoBack
}