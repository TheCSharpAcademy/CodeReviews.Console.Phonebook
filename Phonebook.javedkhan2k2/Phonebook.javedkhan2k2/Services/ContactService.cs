using Phonebook.Data;
using Phonebook.Entities;
using Phonebook.Repositories;
using Spectre.Console;
using Microsoft.Extensions.Configuration;

namespace Phonebook.Services;

public class ContactService
{
    private ContactRepository _contactRepository;
    private IConfigurationRoot _config;
    private SmsService _smsService;
    private EmailService _emailService;

    public ContactService(PhonebookDbContext context, IConfigurationRoot config)
    {
        _contactRepository = new ContactRepository(context);
        _config = config;
        _smsService = new SmsService(_config["twilioAccountSid"], _config["twilioAuthToken"], _config["twilioNumber"]);
        _emailService = new EmailService(_config["senderEmail"], _config["senderPassword"], _config["smtpHost"], Convert.ToInt32(_config["smtpPort"]));
    }

    public void AddContact(IEnumerable<ContactCategory> contactCategories)
    {
        var contact = UserInput.GetNewContact(contactCategories);
        if (contact == null)
        {
            AnsiConsole.Markup("You canceled the Operation\n");
            VisualizationEngine.DisplayContinueMessage();
            return;
        }
        _contactRepository.AddContact(contact);
        AnsiConsole.Markup($"Contact {contact.Name} {contact.Email} {contact.PhoneNumber} Added [green]Successfully[/]");
        VisualizationEngine.DisplayContinueMessage();
    }

    internal void UpdateContact(IEnumerable<ContactCategory> contactCategories)
    {
        var contacts = _contactRepository.GetAllContacts();
        VisualizationEngine.DisplayContacts(contacts, "Contacts Table");
        var id = UserInput.GetIntInput();
        // Validation goes here
        var contact = contacts.FirstOrDefault(x => x.Id == id);
        if (contact == null)
        {
            AnsiConsole.Markup($"Contact with id {id} not found!");
            Console.ReadLine();
            return;
        }
        if (UserInput.UpdateContact(contact, contactCategories))
        {
            _contactRepository.UpdateContact(contact);
            AnsiConsole.Markup($"Contact {contact.Name} {contact.Email} {contact.PhoneNumber} Updated [green]Successfully[/]\n");
        }
        else
        {
            AnsiConsole.Markup("You canceled the Operation\n");
        }
        VisualizationEngine.DisplayContinueMessage();
    }

    internal void DeleteContact()
    {
        var contacts = _contactRepository.GetAllContacts();
        VisualizationEngine.DisplayContacts(contacts, "Contacts Table");
        var id = UserInput.GetIntInput();
        var contact = _contactRepository.GetContactById(id);
        if (contact == null)
        {
            AnsiConsole.Markup($"Contact with id [maroon]{id}[/] not found!");
            VisualizationEngine.DisplayContinueMessage();
            return;
        }
        _contactRepository.DeleteContact(contact);
        AnsiConsole.Markup($"Contact {contact.Name} {contact.Email} {contact.PhoneNumber} Updated [green]Successfully[/]");
        VisualizationEngine.DisplayContinueMessage();
    }

    internal void ViewAllContacts()
    {
        var contacts = _contactRepository.GetAllContacts();
        VisualizationEngine.DisplayContacts(contacts, "Contacts Table");
        VisualizationEngine.DisplayContinueMessage();
    }

    internal void SendSms()
    {
        var contacts = _contactRepository.GetAllContacts();
        VisualizationEngine.DisplayContacts(contacts, "Contacts Table");
        var id = UserInput.GetIntInput();
        var contact = _contactRepository.GetContactById(id);
        if (contact == null)
        {
            AnsiConsole.Markup($"Contact with id [maroon]{id}[/] not found!");
            VisualizationEngine.DisplayContinueMessage();
            return;
        }
        VisualizationEngine.DisplayContactDetail(contact, "Contact Details");
        var messageBody = AnsiConsole.Ask<string>($"Write a text to Send to {contact.Name}.\n");

        _smsService.SendSms(messageBody, contact.PhoneNumber);
    }

    internal void SendEmail()
    {
        var contacts = _contactRepository.GetAllContacts();
        VisualizationEngine.DisplayContacts(contacts, "Contacts Table");
        var id = UserInput.GetIntInput();
        var contact = _contactRepository.GetContactById(id);
        if (contact == null)
        {
            AnsiConsole.Markup($"Contact with id [maroon]{id}[/] not found!");
            VisualizationEngine.DisplayContinueMessage();
            return;
        }
        VisualizationEngine.DisplayContactDetail(contact, "Contact Details");
        var subject = AnsiConsole.Ask<string>("Enter a Subject for the email: ");
        var messageBody = AnsiConsole.Ask<string>($"Write a text to Send to {contact.Name} - {contact.Email}:");
        _emailService.SendEmail(subject, messageBody, contact.Email);        
    }

}