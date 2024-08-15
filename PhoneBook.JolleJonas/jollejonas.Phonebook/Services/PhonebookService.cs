using jollejonas.Phonebook.Models;
using jollejonas.Phonebook.Data;
using jollejonas.Phonebook.UserInput;
using Spectre.Console;

namespace jollejonas.Phonebook.Services;

public class PhonebookService
{
    private readonly PhonebookContext _context;

    public PhonebookService(PhonebookContext context)
    {
        _context = context;
    }
    enum MessageType
    {
        Email,
        Sms
    }

    public void Run()
    {
        while (true) { 
        Console.Clear();
        var menuOptions = new List<MenuOption>
        {
            new MenuOption("Add Contact", AddContact),
            new MenuOption("Display Contacts", DisplayContacts),
            new MenuOption("Update Contact", UpdateContact),
            new MenuOption("Delete Contact", DeleteContact),
            new MenuOption("Display Categories", DisplayCategories),
            new MenuOption("Send Message", SendMessage),
            new MenuOption("Exit", Exit)
        };

        var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOption>()
                .Title("Select an option:")
                .AddChoices(menuOptions)
                .UseConverter(o => o.Name));

        selectedOption.Action.Invoke();

            if(selectedOption.Name == "Exit")
            {
                break;
            }
        }
    }

    public void AddContact()
    {
        string? name = ContactInput.GetName();
        if (name == null) return;

        string? phoneNumber = ContactInput.GetPhoneNumber();
        if (phoneNumber == null) return;

        string? email = ContactInput.GetEmail();
        if (email == null) return;

        string? note = ContactInput.GetNote();
        if (note == null) return;

        int categoryId = ContactInput.GetCategoryId(GetCategories());

        var contact = new Contact
        {
            Name = name,
            PhoneNumber = phoneNumber,
            Email = email,
            Note = note,
            CategoryId = categoryId
        };

        try
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();

            Console.WriteLine("Contact added!");
        }
        catch (Exception)
        {

            throw;
        }
    }

    public List<Contact> GetContacts()
    {
        List<Contact> contacts = new List<Contact>();
        contacts = _context.Contacts.ToList();
        if (contacts.Count == 0)
        {
            Console.WriteLine("No contacts found");
            Console.ReadKey();
            return null;
        }
        return contacts;
    }

    public List<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }

    public void DisplayContacts()
    {
       var contacts = GetContacts();

        if (contacts == null) return;
        foreach (var contact in contacts)
        {
            Console.WriteLine($"Name: {contact.Name}");
            Console.WriteLine($"Phone number: {contact.PhoneNumber}");
            Console.WriteLine($"Email: {contact.Email}");
            Console.WriteLine($"Note: {contact.Note}");
            Console.WriteLine($"Category: {contact.Category.Name}");
            Console.WriteLine();
        }
    }

    public void DisplayCategories()
    {
        var categories = GetCategories();

        foreach (var category in categories)
        {
            Console.WriteLine($"{category.Id}: {category.Name}");
        }
    }

    public void UpdateContact()
    {
        Contact contact = ContactInput.GetContact(GetContacts());

        if (contact == null) return;
        contact.Name = ContactInput.GetName();
        if (contact.Name == null) return;

        contact.PhoneNumber = ContactInput.GetPhoneNumber();
        if (contact.PhoneNumber == null) return;

        contact.Email = ContactInput.GetEmail();
        if (contact.Email == null) return;

        contact.Note = ContactInput.GetNote();
        if (contact.Note == null) return;

        contact.CategoryId = ContactInput.GetCategoryId(GetCategories());

        if (!ContactInput.ConfirmEditDelete())
        {
            Console.WriteLine("Update cancelled.");
            return;
        }

        _context.SaveChanges();
        Console.WriteLine("Contact updated successfully!");
    }


    public void DeleteContact()
    {
        Contact contact = ContactInput.GetContact(GetContacts());
        if (contact == null) return;

        if (!ContactInput.ConfirmEditDelete())
        {
            Console.WriteLine("Delete cancelled.");
            return;
        }
        _context.Contacts.Remove(contact);
        _context.SaveChanges();
        Console.WriteLine("Contact deleted successfully!");
    }

    public void Exit()
    {
        AnsiConsole.MarkupLine("[red]Goodbye![/]");
        Environment.Exit(0);
    }

    public void SendMessage()
    {
        List<Contact> contacts = GetContacts();
        if (contacts == null) return;
        Console.WriteLine("Who do you want to send message?");

        Contact contact = ContactInput.GetContact(contacts);
        var messageType = AnsiConsole.Prompt(
                new SelectionPrompt<MessageType>()
                    .Title("Select message type")
                    .PageSize(6)
                    .AddChoices(new[] { MessageType.Email, MessageType.Sms })
            );

        switch (messageType)
        {
            case MessageType.Email:
                EmailService.SendEmail(contact.Email, contact.Name);
                break;
            case MessageType.Sms:
                SmsService.SendSms(contact.PhoneNumber);
                break;
            default:
                throw new NotImplementedException();
        }


    }
}
