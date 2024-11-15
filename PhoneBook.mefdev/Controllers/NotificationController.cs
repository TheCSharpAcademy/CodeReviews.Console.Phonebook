
using PhoneBook.mefdev.Models;
using PhoneBook.mefdev.Service;
using Spectre.Console;

namespace PhoneBook.mefdev.Controllers;

internal class NotificationController: BaseController
{
    private ContactService _contactService { get; set; }

    public NotificationController(ContactService contactService)
	{
        _contactService = contactService;
    }
	public void SendNotification()
	{
        RenderCustomLine("DodgerBlue1", "SEND A NOTIFICATION");

        var contacts = _contactService.GetContacts();
        var contactName = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
           .Title("Select a [red]contact[/] to send a notification to")
           .PageSize(10)
           .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
           .AddChoices(contacts.Select(c => c.Name)));
        var contact = _contactService.GetContactByName(contactName);
        if (contact == null)
        {
            DisplayMessage("Contact is not found", "red");
            return;
        }
        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
              .Title("Select notification type:")
              .PageSize(5)
              .AddChoices(
                NotificationType.SMS.ToString(),
                NotificationType.Email.ToString()));

        NotificationType notificationType;

        if (choice.Equals("SMS"))
        {
            notificationType = NotificationType.SMS;
        }
        else if (choice.Equals("Email"))
        {
            notificationType = NotificationType.Email;
        }
        else
        {
            DisplayMessage("Invalid choice.", "red");
            return;
        }
        var body = AnsiConsole.Prompt<string>(new TextPrompt<string>("Enter a body: "));

        var notificationService = InitializeNotificationService();
        notificationService.Notify(contact, body, notificationType);

        DisplayMessage("Notification has been sent.", "green");
    }

    private NotificationService InitializeNotificationService()
    {
        var twilioAccountSid = GetEnvVar("ACCOUNT_SID");
        var twilioAuthToken = GetEnvVar("AUTH_TOKEN");
        var twilioFromPhoneNumber = GetEnvVar("PHONE_NUMBER");

        var smsSender = new TwilioSmsSender(twilioAccountSid, twilioAuthToken, twilioFromPhoneNumber);

        var apiKey = GetEnvVar("SENDGRID_API_KEY");
        var company = GetEnvVar("COMPANY");
        var fromEmail = GetEnvVar("FROM_EMAIL");

        var emailSender = new EmailSender(apiKey, company, fromEmail);

        return new NotificationService(smsSender, emailSender);
    }

    private string GetEnvVar(string key, string defaultValue = null)
    {
        var value = Environment.GetEnvironmentVariable(key);
        return string.IsNullOrEmpty(value) ? defaultValue : value;
    }

}
