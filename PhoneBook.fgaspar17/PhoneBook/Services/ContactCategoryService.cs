using PhoneBookLibrary;

namespace PhoneBook;

public class ContactCategoryService
{
    public static void AssigningContactCategory()
    {
        MenuPresentation.PresentMenu("[blue]Assigning[/]");
        bool isCancelled;
        string contactName;
        Category categorySelected;
        Contact contactSelected;

        categorySelected = AskForCategory();
        if (categorySelected == null) return;

        ContactService.ShowContactTable();

        ExistingModelValidator<string, Contact> existingContact = new()
        {
            ErrorMsg = "Contact Name doesn't exist.",
            GetModel = ContactController.GetContactByName,
        };

        UniquePropertyValidator<string, Category> uniqueContactCategory = new()
        {
            ErrorMsg = $"Contact must be unique for the Category {categorySelected.Name}.",
            GetModel = ContactCategoryController.GetCategoryByContactName,
            PropertyName = "Name",
        };

        (isCancelled, contactName) = ContactService.AskForContactName(uniqueContactCategory, existingContact);
        if (isCancelled) return;

        contactSelected = ContactController.GetContactByName(contactName);

        ContactCategoryController.InsertContactCategory(new ContactCategory
        {
            ContactId = contactSelected.Id,
            Contact = contactSelected,
            CategoryId = categorySelected.Id,
            Category = categorySelected,
        });
    }

    public static void DeleteContactCategory()
    {
        MenuPresentation.PresentMenu("[red]Deleting[/]");
        bool isCancelled;
        string contactName;
        Category categorySelected;
        Contact contactSelected;

        categorySelected = AskForCategory();
        if (categorySelected == null) return;

        ShowContactsByCategoryTable(categorySelected);

        ExistingModelValidator<string, Category> existingContactCategory = new()
        {
            ErrorMsg = $"Contact Name doesn't exist for {categorySelected.Name} Category.",
            GetModel = ContactCategoryController.GetCategoryByContactName
        };

        (isCancelled, contactName) = ContactService.AskForContactName(existingContactCategory);
        if (isCancelled) return;

        contactSelected = ContactController.GetContactByName(contactName);

        ContactCategoryController.DeleteContactCategory(new ContactCategory
        {
            ContactId = contactSelected.Id,
            Contact = contactSelected,
            CategoryId = categorySelected.Id,
            Category = categorySelected,
        });
    }

    public static void ShowContactsByCategory()
    {
        Category categorySelected = AskForCategory();
        if (categorySelected == null) return;

        ShowContactsByCategoryTable(categorySelected);
        Prompter.PressKeyToContinuePrompt();
    }

    public static void ShowCategoriesByContact()
    {
        bool isCancelled;
        string contactName;

        ContactService.ShowContactTable();

        ExistingModelValidator<string, Contact> existingContact = new()
        {
            ErrorMsg = "Contact Name doesn't exist.",
            GetModel = ContactController.GetContactByName
        };

        (isCancelled, contactName) = ContactService.AskForContactName(existingContact);
        if (isCancelled) return;

        ShowCategoriesByContact(ContactController.GetContactByName(contactName));
        Prompter.PressKeyToContinuePrompt();
    }

    private static void ShowContactsByCategoryTable(Category category)
    {
        List<ContactDto> contacts = ContactCategoryController.GetContactsByCategoryId(category.Id).Select(c => ContactMapper.MapToDto(c)).ToList();
        OutputRenderer.ShowTable(contacts, $"Contacts In {category.Name}");
    }

    private static void ShowCategoriesByContact(Contact contact)
    {
        List<CategoryDto> categories = ContactCategoryController.GetCategoriesByContactId(contact.Id).Select(c => CategoryMapper.MapToDto(c.Category)).ToList();
        OutputRenderer.ShowTable(categories, $"Categories For {contact.Name}");
    }

    private static Category? AskForCategory()
    {
        bool isCancelled;
        string categoryName;

        CategoryService.ShowCategoryTable();

        ExistingModelValidator<string, Category> existingCategory = new()
        {
            ErrorMsg = "Category Name doesn't exist.",
            GetModel = CategoryController.GetCategoryByName
        };

        (isCancelled, categoryName) = CategoryService.AskForCategoryName(existingCategory);
        if (isCancelled) return null;

        return CategoryController.GetCategoryByName(categoryName);
    }
}