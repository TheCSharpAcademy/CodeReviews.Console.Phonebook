namespace PhoneBook;

using System.Net.Mail;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

class MessageController
{
    private readonly MainController mainController;

    public MessageController(MainController mainController)
    {
        this.mainController = mainController;
    }

    public void ShowCreateMail(Contact contact)
    {
        var view = new MessageCreateMailView(this, contact);
        view.Show();
    }

    public void SendMail(Contact contact, string? subject, string? body)
    {
        if (string.IsNullOrEmpty(contact.Email))
        {
            ShowContactDetails(contact, "Contact-Email must not be empty.");
            return;
        }

        if (string.IsNullOrEmpty(subject))
        {
            ShowContactDetails(contact, "Mail-Subject must not be empty.");
            return;
        }

        if (string.IsNullOrEmpty(body))
        {
            ShowContactDetails(contact, "Mail-Body must not be empty.");
            return;
        }

        var config = Configuration.GetInstance();

        MailMessage message = new MailMessage(config.MailFrom, contact.Email)
        {
            Subject = subject,
            IsBodyHtml = false,
            Body = body
        };

        SmtpClient client = new SmtpClient(config.MailHost, config.MailPort);
        if (!string.IsNullOrEmpty(config.MailUsername))
        {
            client.Credentials = new System.Net.NetworkCredential(config.MailUsername, config.MailPassword);
        }

        try
        {
            client.Send(message);
        }
        catch (Exception)
        {
            ShowContactDetails(contact, $"ERROR - Failed to send email to '{contact.Name}'");
            return;
        }
        ShowContactDetails(contact, $"OK - Email sent to '{message.To}'");
    }

    public void ShowCreateSms(Contact contact)
    {
        var view = new MessageCreateSmsView(this, contact);
        view.Show();
    }

    public void SendSms(Contact contact, string? text)
    {
        if (string.IsNullOrEmpty(contact.MobileNumber))
        {
            ShowContactDetails(contact, "Mobile Number must not be empty.");
            return;
        }

        if (string.IsNullOrEmpty(text))
        {
            ShowContactDetails(contact, "Text must not be empty.");
            return;
        }

        var config = Configuration.GetInstance();

        try
        {
            TwilioClient.Init(config.TwilioAccountSid, config.TwilioAuthToken);
            var message = MessageResource.Create(
                from: new Twilio.Types.PhoneNumber(config.TwilioFrom),
                body: text,
                to: new Twilio.Types.PhoneNumber(contact.MobileNumber)
            );
            ShowContactDetails(contact, $"OK - SMS sent to '{contact.MobileNumber}'");
        }
        catch (Exception)
        {
            ShowContactDetails(contact, $"ERROR - Failed to send SMS to '{contact.Name}'");
            return;
        }

    }

    public void ShowContactDetails(Contact contact)
    {
        ShowContactDetails(contact, null);
    }

    public void ShowContactDetails(Contact contact, string? message)
    {
        mainController.ShowContactDetails(contact, message);
    }
}