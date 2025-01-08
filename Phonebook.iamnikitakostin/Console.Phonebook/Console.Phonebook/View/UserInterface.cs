using Console.Phonebook.Controllers;
using Console.Phonebook.Models;
using Console.Phonebook.Services;
using Console.Phonebook.Validations;
using Spectre.Console;

namespace Console.Phonebook.View;

internal class UserInterface : ConsoleController
{
    private readonly ContactService _contactService;
    private readonly CategoryService _categoryService;
    private readonly MessageService _messageService;

    public UserInterface(ContactService contactService, CategoryService categoryService, MessageService messageService)
    {
        _contactService = contactService;
        _categoryService = categoryService;
        _messageService = messageService;
    }

    internal void MainMenu()
    {
        var menuOptions = EnumToDisplayNames<MainMenuOptions>();
        while (true)
        {
            AnsiConsole.Clear();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                .Title("What do you want to do next?")
                .AddChoices(menuOptions.Keys)
                .UseConverter(option => menuOptions[option]));

            switch (choice)
            {
                case MainMenuOptions.CurrentContacts:
                    Contact? contact = ShowCurrentContacts();
                    if (contact == null)
                        break;

                    ContactMenu(contact);

                    break;
                case MainMenuOptions.AddContact:
                    AddContactMenu();
                    break;
                case MainMenuOptions.ManageCategories:
                    ManageCategories();
                    break;
                case MainMenuOptions.SendMessage:
                    SendMessageMenu();
                    break;
                default:
                    return;
            }
        }
    }

    internal void ManageCategories()
    {
        var menuOptions = EnumToDisplayNames<ManageCategoriesOptions>();

        while (true)
        {
            AnsiConsole.Clear();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<ManageCategoriesOptions>()
                .Title("What do you want to do next?")
                .AddChoices(menuOptions.Keys)
                .UseConverter(option => menuOptions[option]));

            switch (choice)
            {
                case ManageCategoriesOptions.Add:
                    AddCategory();
                    break;
                case ManageCategoriesOptions.Delete:
                    Dictionary<int, string> categoryOptions = _categoryService.GetCategories();

                    var categoryId = AnsiConsole.Prompt(
                        new SelectionPrompt<int>()
                        .Title("Select a category: ")
                        .AddChoices(categoryOptions.Keys)
                        .UseConverter(option => categoryOptions[option]));

                    Category category = _categoryService.GetCategoryById(categoryId);
                    DeleteCategory(category);

                    break;
                default:
                    return;
            }
        }
    }

    internal void DeleteCategory(Category category)
    {
        bool confirmation = ConfirmDeletion($"category of {category.Name}");

        if (!confirmation)
            return;

        bool wasRequestSuccesfull = _categoryService.Delete(category.Id);

        if (wasRequestSuccesfull)
        {
            SuccessMessage("The category has been deleted");
        }
    }

    internal void EditMenu(Contact contact)
    {
        var menuOptions = EnumToDisplayNames<EditMenuOptions>();

        while (true)
        {
            AnsiConsole.Clear();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<EditMenuOptions>()
                .Title("What do you want to do next?")
                .AddChoices(menuOptions.Keys)
                .UseConverter(option => menuOptions[option]));

            switch (choice)
            {
                case EditMenuOptions.Email:
                    string newEmail = AnsiConsole.Prompt(new TextPrompt<string>("Enter a new Email for the contact:"));
                    while (!Validation.Email(newEmail))
                    {
                        newEmail = AnsiConsole.Prompt(new TextPrompt<string>("Email format should be 'abc@mail.ru', please re-enter a new Email in the correct format:"));
                    }
                    contact.Email = newEmail;
                    _contactService.Update(contact);
                    break;
                case EditMenuOptions.Name:
                    string newName = AnsiConsole.Prompt(new TextPrompt<string>("Enter a new Name for the contact:"));
                    contact.Name = newName;
                    _contactService.Update(contact);
                    break;
                case EditMenuOptions.PhoneNumber:
                    string newPhoneNumber = AnsiConsole.Prompt(new TextPrompt<string>("Enter a new Phone Number:"));
                    while (!Validation.PhoneNumber(newPhoneNumber))
                    {
                        newPhoneNumber = AnsiConsole.Prompt(new TextPrompt<string>("Phone Number format should be '+128340', having at least 5 characters, please re-enter a new Phone Number in the correct format:"));
                    }
                    contact.PhoneNumber = newPhoneNumber;
                    _contactService.Update(contact);
                    break;
                case EditMenuOptions.Category:
                    Dictionary<int, string> categoryOptions = _categoryService.GetCategories();

                    var categoryId = AnsiConsole.Prompt(
                        new SelectionPrompt<int>()
                        .Title("Select a new category: ")
                        .AddChoices(categoryOptions.Keys)
                        .UseConverter(option => categoryOptions[option]));

                    Category newCategory = _categoryService.GetCategoryById(categoryId);

                    contact.Category = newCategory;
                    _contactService.Update(contact);
                    break;
                default:
                    return;
            }
        }
    }

    internal void DeleteContact(Contact contact)
    {
        bool confirmation = ConfirmDeletion($"contact of {contact.Name}");

        if (!confirmation)
            return;

        bool wasRequestSuccesfull = _contactService.Delete(contact.Id);

        if (wasRequestSuccesfull)
        {
            SuccessMessage("The contact has been deleted");
        }
    }

    internal void AddCategory()
    {
        Category newCategory = new();

        string name = AnsiConsole.Prompt(new TextPrompt<string>("Enter a Name for the category:"));

        newCategory.Name = name;

        bool isSuccess = _categoryService.Add(newCategory);

        if (isSuccess)
        {
            SuccessMessage("A new category has been added");
        }
    }

    internal void AddContactMenu()
    {
        Contact newContact = new();

        string name = AnsiConsole.Prompt(new TextPrompt<string>("Enter a Name for the contact:"));

        string email = AnsiConsole.Prompt(new TextPrompt<string>("Enter an Email for the contact:"));
        while (!Validation.Email(email))
        {
            email = AnsiConsole.Prompt(new TextPrompt<string>("Enter an Email for the contact:"));
        }

        string phoneNumber = AnsiConsole.Prompt(new TextPrompt<string>("Enter a Phone Number for the contact starting with a + (e.g. +13194127914):"));
        while (!Validation.PhoneNumber(phoneNumber))
        {
            phoneNumber = AnsiConsole.Prompt(new TextPrompt<string>("Enter a Phone Number for the contact starting with a + (e.g. +13194127914):"));
        }


        Dictionary<int, string> categoryOptions = _categoryService.GetCategories();
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
            .Title("Select a category for the contact: ")
            .AddChoices(categoryOptions.Keys)
            .UseConverter(option => categoryOptions[option]));

        newContact.Name = name;
        newContact.PhoneNumber = phoneNumber;
        newContact.Email = email;
        newContact.CreatedDate = DateTime.Now;
        newContact.Category = _categoryService.GetCategoryById(choice);
        AnsiConsole.Write(newContact.Category.Name);

        bool isSuccess = _contactService.Add(newContact);

        if (isSuccess)
        {
            SuccessMessage("Your new contact has been added.");
        }
    }

    internal void SendMessageMenu()
    {
        var menuOptions = EnumToDisplayNames<SendMessageOptions>();
        while (true)
        {
            AnsiConsole.Clear();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<SendMessageOptions>()
                .Title("What kind of message would you like to send?")
                .AddChoices(menuOptions.Keys)
                .UseConverter(option => menuOptions[option]));

            switch (choice)
            {
                case SendMessageOptions.Email:
                    SendEmail();
                    break;
                case SendMessageOptions.SMS:
                    SendSMS();
                    break;
                default:
                    return;
            }
        }
    }

    internal void SendSMS()
    {
        bool isPhoneConfigured = _messageService.CheckIfPhoneConfigured();

        if (!isPhoneConfigured)
            return;

        Contact contact = ShowCurrentContacts();

        if (contact.PhoneNumber == null)
        {
            ErrorMessage("Contact's phone number is not set.\nSending an SMS is impossible.");
            return;
        }

        string text = AnsiConsole.Prompt(new TextPrompt<string>("Enter text of the SMS:").Validate(e => e.Length > 1));

        bool isSuccess = _messageService.SendSMS(contact.Email, text);

        if (isSuccess)
            SuccessMessage("Message has been sent. Press any button to continue...");
    }

    internal void SendEmail()
    {
        bool isEmailConfigured = _messageService.CheckIfEmailConfigured();

        if (!isEmailConfigured)
            return;

        Contact contact = ShowCurrentContacts();

        if (contact.Email == null)
        {
            ErrorMessage("Contact's email is not set.\nSending an email is impossible.");
            return;
        }

        string subject = AnsiConsole.Prompt(new TextPrompt<string>("Enter a subject of the Email:").Validate(e => e.Length > 1));
        string text = AnsiConsole.Prompt(new TextPrompt<string>("Enter text of the Email:").Validate(e => e.Length > 1));

        bool isSuccess = _messageService.SendEmail(contact.Email, subject, text);

        if (isSuccess)
            SuccessMessage("Message has been sent. Press any button to continue...");
    }

    internal void ContactMenu(Contact contact)
    {
        var menuOptions = EnumToDisplayNames<ContactMenuOptions>();
        while (true)
        {
            AnsiConsole.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<ContactMenuOptions>()
                .Title("What do you want to do next?")
                .AddChoices(menuOptions.Keys)
                .UseConverter(option => menuOptions[option]));

            switch (choice)
            {
                case ContactMenuOptions.ViewFull:
                    VisualizeContact(contact);
                    break;
                case ContactMenuOptions.Edit:
                    EditMenu(contact);
                    break;
                case ContactMenuOptions.Delete:
                    DeleteContact(contact);
                    return;
                default:
                    return;
            }
        }
    }

    internal Contact? ShowCurrentContacts()
    {
        List<Contact> contacts = _contactService.GetAll();

        if (contacts.Count < 1)
        {
            ErrorMessage("There are no contacts found, please add one first");
            return null;
        }

        Dictionary<int, string> contactsOptions = contacts.ToDictionary(c => c.Id, c => c.Name);

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
            .Title("Select a contact: ")
            .AddChoices(contactsOptions.Keys)
            .UseConverter(option => contactsOptions[option]));

        Contact chosenContact = _contactService.GetById(choice);

        return chosenContact;
    }

    internal static void VisualizeContact(Contact contact)
    {
        Table table = new Table();

        table.AddColumns("Id", "Name", "Email", "Phone", "Creation Date", "Category");

        table.AddRow(contact.Id.ToString(), contact.Name, contact.Email, contact.PhoneNumber, contact.CreatedDate.ToString(), contact.Category.Name);

        AnsiConsole.Write(table);
        AnsiConsole.Write("Press any key to continue...");
        AnsiConsole.Console.Input.ReadKey(false);
    }

    static Dictionary<TEnum, string> EnumToDisplayNames<TEnum>() where TEnum : struct, Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .ToDictionary(
                value => value,
                value => SplitCamelCase(value.ToString())
            );
    }

    internal static string SplitCamelCase(string input)
    {
        return string.Join(" ", System.Text.RegularExpressions.Regex
            .Split(input, @"(?<!^)(?=[A-Z])"));
    }
}

