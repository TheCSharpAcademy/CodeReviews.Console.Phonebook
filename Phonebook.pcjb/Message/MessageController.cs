namespace PhoneBook;

using System.Net.Mail;

class MessageController
{
    private ContactController? contactController;

    public void ShowCreateMail(Contact contact)
    {
        var view = new MessageCreateMailView(this, contact);
        view.Show();
    }

    public void SetContactController(ContactController contactController)
    {
        this.contactController = contactController;
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

    public void ShowContactDetails(Contact contact)
    {
        ShowContactDetails(contact, null);
        }

    public void ShowContactDetails(Contact contact, string? message)
    {
        if (contactController == null)
        {
            throw new InvalidOperationException("Required ContactController missing.");
        }
        contactController.ShowDetails(contact.ContactID, message);
    }
}