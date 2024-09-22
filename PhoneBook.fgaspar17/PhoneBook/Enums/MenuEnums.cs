namespace PhoneBook;

public enum ContactMenuOptions
{
    [Title("Quit")]
    Quit,
    [Title("Create Contact")]
    CreateContact,
    [Title("Update Contact")]
    UpdateContact,
    [Title("Delete Contact")]
    DeleteContact,
    [Title("Show Contact")]
    ShowContact,
    [Title("Show Contacts")]
    ShowContacts,
    [Title("Manage Categories")]
    ManageCategories,
    [Title("Group Contact by Categories")]
    GroupContactsByCategories,
    [Title("Send Mail")]
    SendMail,
    [Title("Send Sms")]
    SendSms,
}

public enum CategoryMenuOptions
{
    [Title("Back")]
    Back,
    [Title("Create Category")]
    CreateCategory,
    [Title("Update Category")]
    UpdateCategory,
    [Title("Delete Category")]
    DeleteCategory,
    [Title("Show Categories")]
    ShowCategories,
}

public enum GroupContactsByCategoriesMenuOptions
{
    [Title("Back")]
    Back,
    [Title("Assign Category to a Contact")]
    AssignCategoryToContact,
    [Title("Remove Category from Contact")]
    RemoveCategoryFromContact,
    [Title("Show Categories for Contact")]
    ShowCategoriesForContact,
    [Title("Show Contacts in Category")]
    ShowContactsInCategory,
}