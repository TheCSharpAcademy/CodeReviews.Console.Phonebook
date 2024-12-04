using MimeKit;
using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook;

public class PhonebookController
{
    private readonly DisplayData _displayData;
    private readonly UserInput _userInput;
    private readonly ContactsDataManager _dataManager;
    private readonly CategoryDataManager _categoryDataManager;

    public PhonebookController(DisplayData displayData, UserInput userInput, ContactsDataManager dataManager, CategoryDataManager categoryDataManager)
    {
        _displayData = displayData;
        _userInput = userInput;
        _dataManager = dataManager;
        _categoryDataManager = categoryDataManager;
    }

    internal void ShowMainMenu()
    {
        bool closeApp = false;
        while (closeApp == false)
        {
            int inputNumber = _userInput.MainMenu();
            switch (inputNumber)
            {
                case 0:
                    Console.WriteLine("\nBye!\n");
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                case 1:
                    AddContact();
                    break;
                case 2:
                    DeleteContact();
                    break;
                case 3:
                    UpdateContact();
                    break;
                case 4:
                    ViewContacts();
                    break;
                case 5:
                    AddCategory();
                    break;
                case 6:
                    ViewCategories();
                    break;
                case 7:
                    DeleteCategory();
                    break;
                case 8:
                    UpdateCategory();
                    break;
                case 9:
                    SendEmail();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\nInvalid Command. Give me number!");
                    break;
            }
        }
    }

    private void AddContact()
    {
        Contact contact = new Contact
        {
            Name = _userInput.GetName("contact"),
            Email = _userInput.GetContactEmail(),
            PhoneNumber = _userInput.GetPhoneNumber()
        };

        List<Category> categories = CategoryDataManager.GetCategories();
        Category category = categories.Any() ? _userInput.GetCategory() : AddCategory();

        if (category.Name == "Create a new category")
            category = AddCategory();

        contact.CategoryId = category.CategoryId;
        _dataManager.AddNewContact(contact);
        Console.Clear();
        Console.WriteLine($"Contact added");
        _userInput.PressToContinue();
    }

    private void DeleteContact()
    {
        List<Contact> contacts = _dataManager.GetContacts();
        if (contacts.Count() == 0)
            NothingFound("contacts");
        string chooseMessage = "Choose contact to be elminated: ";
        Contact chosenOne = _userInput.GetContact(contacts, chooseMessage);
        List<Contact> chosenList = new List<Contact> { chosenOne };
        _displayData.ShowContacts(chosenList);
        string deleteMessage = "Are you sure you want to delete this contact?";
        if (_userInput.GetConfirmation(deleteMessage))
            _dataManager.DeleteContact(chosenOne);
    }

    private void ViewContacts()
    {
        List<Contact> contacts = _dataManager.GetContacts();
        _displayData.ShowContacts(contacts);
        _userInput.PressToContinue();
    }

    private void UpdateContact()
    {
        List<Contact> contacts = _dataManager.GetContacts();
        if (contacts.Count() == 0)
            NothingFound("contacts");
        string message = "Choose contact to update: ";
        Contact chosenOne = _userInput.GetContact(contacts, message);
        List<Contact> chosenList = new List<Contact> { chosenOne };
        int updateChoice;

        do
        {
            _displayData.ShowContacts(chosenList);
            updateChoice = _userInput.ChooseContactToUpdate();
            Action performAction = updateChoice switch
            {
                0 => () => ShowMainMenu(),
                1 => () => chosenOne.Name = _userInput.GetName("contact"),
                2 => () => chosenOne.PhoneNumber = _userInput.GetPhoneNumber(),
                3 => () => chosenOne.Email = _userInput.GetContactEmail(),
                4 => () => chosenOne.Category.Name = _userInput.GetName("category"),
                5 => () =>
                {
                    _dataManager.UpdateContact(chosenOne);
                    Console.WriteLine($"\nChanges committed!\n");
                    _userInput.PressToContinue();
                }
                ,
                _ => () => Console.WriteLine($"I don't know how you even got here. Retreat!")
            };
            performAction();
        } while (updateChoice != 0);
    }

    internal Category AddCategory()
    {
        Category category = new Category();
        category.Name = _userInput.GetName("category");
        CategoryDataManager.AddCategory(category);
        return category;
    }

    internal void DeleteCategory()
    {
        int categoryId;
        List<Category> categories = CategoryDataManager.GetCategories();
        if (categories.Count() == 1)
            categoryId = categories.First().CategoryId;
        if (categories.Count() == 0)
            NothingFound("categories");
        categoryId = _userInput.GetCategory().CategoryId;
        CategoryDataManager.RemoveCategory(categoryId);
    }

    internal void ViewCategories()
    {
        List<Category> categories = CategoryDataManager.GetCategories();
        _displayData.ShowCategories(categories);
        _userInput.PressToContinue();
    }

    internal void UpdateCategory()
    {
        List<Category> categories = CategoryDataManager.GetCategories();
        if (categories.Count() == 0)
            NothingFound("categories");
        Category category = _userInput.GetCategory();
        category.Name = _userInput.GetName("new category");
        CategoryDataManager.UpdateCategory(category);
    }

    internal void SendEmail()
    {
        List<Contact> contacts = _dataManager.GetContacts();
        if (contacts.Count() == 0)
            NothingFound("contacts");
        Contact emailReceiver = _userInput.GetContact(contacts, "Choose contact to send an email");

        var emailMessage = new MimeMessage();
        Email email = _userInput.GetEmailDetails();
        email.ToAddress = emailReceiver.Email;
        _displayData.ShowEmail(email);

        if (_userInput.Confirm("Confirm to send email draft:"))
        {
            emailMessage.From.Add(new MailboxAddress(email.FromAddress));
            emailMessage.To.Add(new MailboxAddress(email.ToAddress));
            emailMessage.Subject = email.Subject;
            emailMessage.Body = new TextPart("plain")
            {
                Text = email.Body
            };
            EmailDataManager.SendEmail(emailMessage);
        }
    }

    internal void NothingFound(string item)
    {
        Console.WriteLine($"No {item} exist.\n");
        _userInput.PressToContinue();
        ShowMainMenu();
    }
}