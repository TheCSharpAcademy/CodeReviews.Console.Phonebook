using Phonebook.Controllers;
using Phonebook.Models;
using Spectre.Console;
using System.Net.Mail;
using System.Net;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using static Phonebook.Models.Enums;

namespace Phonebook.Services;

internal class MessageService
{
    internal static void RouteMessage(Contact contact, Enums.MessagingOptions messagingOption)
    {
        if (messagingOption == MessagingOptions.Email)
        {
            if (contact.Emails.Count <= 0)
            {
                Console.WriteLine("There are no emails for this contact. Press any key to continue");
                Console.ReadKey();
                return;
            }
            Email emailToEmail = EmailServices.GetEmailOptionInput(contact);
            EmailBuilder(emailToEmail, contact);
        }
        else if (messagingOption == MessagingOptions.SMS)
        {
            if (contact.PhoneNumbers.Count <= 0)
            {
                Console.WriteLine("There are no phone numbers for this contact. Press any key to continue");
                Console.ReadKey();
                return;
            }
            Phone phoneToText = PhoneService.GetPhoneOptionInput(contact);
            SmsBuilder(phoneToText, contact);
        }
    }

    private static void SmsBuilder(Phone phoneToText, Contact contact)
    {
        string smsBody = AnsiConsole.Prompt(
        new TextPrompt<string>("SMS Message:")
        .Validate(smsBody =>
        {
            if (!string.IsNullOrEmpty(smsBody))
            {
                return ValidationResult.Success();
            }
            else
            {
                return ValidationResult.Error("[red} Message cannot be empty[/]");
            }
        }));

        bool inMenu = true;
        while (inMenu)
        {
            ViewSms(phoneToText, contact, smsBody);
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<SendMenu>()
                .Title("What would you like to do?")
                .AddChoices(
                    SendMenu.Send,
                    SendMenu.Edit,
                    SendMenu.Discard));

            switch (option)
            {
                case SendMenu.Send:
                    MessageResource message = SmsSend(phoneToText, smsBody);
                    if (message != null)
                    {
                        SmsSave(message, contact);
                    }
                    else
                    {
                        SmsSaveNotSent(phoneToText, smsBody, contact);
                    }
                    inMenu = false;
                    break;
                case SendMenu.Edit:
                    smsBody = EditSms(smsBody);
                    break;
                case SendMenu.Discard:
                    return;
                    break;
            }
        }

    }



    private static MessageResource SmsSend(Phone phoneToText, string smsBody)
    {
        Settings settings = SettingsController.GetSettings();
        if (settings == null)
        {
            Console.WriteLine("Settings are null so you just have to pretend this sent.");
            Console.ReadKey();
            return null;
        }
        else
        {
            try
            {
                TwilioClient.Init(settings.TwilioAccountSid, settings.TwilioAuthToken);

                var message = MessageResource.Create(
                    body: smsBody,
                    from: new Twilio.Types.PhoneNumber(settings.TwilioNumber),
                    to: new Twilio.Types.PhoneNumber(phoneToText.PhoneNumber)
                );

                return message;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending SMS: {ex.Message}");
                Console.WriteLine("Trouble Shooting Notes: Check Settings.  Check Internet Connection.");
                return null;
            }
        }
    }


    private static void SmsSaveNotSent(Phone phoneToText, string smsBody, Contact contact)
    {
        string name = $@"{contact.FirstName} {contact.LastName}";

        SmsHistory sentSms = new SmsHistory();
        sentSms.MessageSid = "not sent";
        sentSms.ContactName = name;
        sentSms.ToNumber = phoneToText.ToString();
        sentSms.SentTS = DateTime.Now;
        sentSms.Body = smsBody;

        SmsHistoryController.AddSmsHistory(sentSms);
    }

    private static void SmsSave(MessageResource message, Contact contact)
    {
        string name = $@"{contact.FirstName} {contact.LastName}";

        SmsHistory sentSms = new SmsHistory();
        sentSms.MessageSid = message.Sid;
        sentSms.ContactName = name;
        sentSms.ToNumber = message.To;
        sentSms.SentTS = DateTime.Now;
        sentSms.Body = message.Body;

        SmsHistoryController.AddSmsHistory(sentSms);
    }

    private static void EmailBuilder(Email emailToEmail, Contact contact)
    {
        Settings settings = SettingsController.GetSettings();

        MailMessage emailMessage = new MailMessage();
        emailMessage.From = new MailAddress(settings.FromMail);
        emailMessage.To.Add(new MailAddress(emailToEmail.EmailAddress));
        emailMessage.Subject = AnsiConsole.Ask<string>("Subject:");
        string body = AnsiConsole.Ask<string>("Email Message Body:");
        emailMessage.Body = $@"<html><body> {body} </body></html>";
        emailMessage.IsBodyHtml = true;


        bool inMenu = true;
        while (inMenu)
        {
            ViewEmail(emailMessage);
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<SendMenu>()
                .Title("What would you like to do?")
                .AddChoices(
                    SendMenu.Send,
                    SendMenu.Edit,
                    SendMenu.Discard));

            switch (option)
            {
                case SendMenu.Send:
                    EmailSend(emailMessage);
                    EmailSave(emailMessage, contact);
                    inMenu = false;
                    break;
                case SendMenu.Edit:
                    emailMessage = EditEmail(emailMessage);
                    break;
                case SendMenu.Discard:
                    inMenu = false;
                    break;
            }
        }
    }

    private static String EditSms(string smsBody)
    {

        smsBody = AnsiConsole.Confirm("Change the text message.")
            ? smsBody = AnsiConsole.Ask<string>("Enter the new text message.")
            : smsBody;

        return smsBody;

    }

    private static MailMessage EditEmail(MailMessage emailMessage)
    {

        emailMessage.Subject = AnsiConsole.Confirm("Change Subject:")
            ? emailMessage.Subject = AnsiConsole.Ask<string>("Add New Subject:")
            : emailMessage.Subject;
        emailMessage.Body = AnsiConsole.Confirm("Change the body of the email:")
            ? emailMessage.Body = AnsiConsole.Ask<string>("Enter the new email body.")
            : emailMessage.Body;

        emailMessage.Body = EmailServices.RemoveHtmlTags(emailMessage.Body);
        emailMessage.Body = $@"<html><body> {emailMessage.Body} </body></html>";

        return emailMessage;
    }

    private static void EmailSave(MailMessage emailMessage, Contact contact)
    {
        EmailHistory emailHistory = new EmailHistory();
        emailHistory.ContactName = $@"{contact.FirstName} {contact.LastName}";
        emailHistory.ToEmail = emailMessage.To.ToString();
        emailHistory.SentTS = DateTime.Now;
        emailHistory.Subject = emailMessage.Subject;
        emailHistory.EmailBody = EmailServices.RemoveHtmlTags(emailMessage.Body);

        EmailHistoryController.AddEmailHistory(emailHistory);

    }

    private static void EmailSend(MailMessage emailMessage)
    {
        Settings settings = SettingsController.GetSettings();
        if (settings == null)
        {
            Console.WriteLine("Settings are null so you just have to pretend this sent.");
            Console.ReadKey();
        }
        else
        {
            try
            {
                var smtpClient = new SmtpClient(settings.SmtpUrl)
                {
                    Port = settings.Port,
                    Credentials = new NetworkCredential(settings.FromMail, settings.Password),
                    EnableSsl = true,
                };


                smtpClient.Send(emailMessage);
                Console.WriteLine("Email sent successfully.");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                Console.WriteLine("Trouble Shooting Notes: Check Settings.  Check Internet Connection.");
                Console.ReadKey();
            }
        }
    }

    private static void ViewSms(Phone phoneToText, Contact contact, string smsBody)
    {
        Console.Clear();

        var table = new Spectre.Console.Table()
            .AddColumn($@"Email")
            .AddRow(new Panel($@"Contact: {contact.FirstName} {contact.LastName}"))
            .AddRow(new Panel($@"To: {phoneToText.PhoneNumber}"))
            .AddRow(new Panel($@"Text: {smsBody}"))
            .Width(60)
            .HideHeaders();

        AnsiConsole.Write(table);
    }
    private static void ViewEmail(MailMessage emailMessage)
    {
        emailMessage.Body = EmailServices.RemoveHtmlTags(emailMessage.Body);
        Console.Clear();


        var table = new Spectre.Console.Table()
            .AddColumn($@"Email")
            .AddRow(new Panel($@"To: {emailMessage.To}"))
            .AddRow(new Panel($@"Subject: {emailMessage.Subject}"))
            .AddRow(new Panel($@"Body: {emailMessage.Body}"))
            .Width(60)
            .HideHeaders();

        AnsiConsole.Write(table);
    }

    internal static void ViewHistory(MessagingOptions messagingOption)
    {

        if (messagingOption == MessagingOptions.Email)
        {
            EmailHistoryService.ShowEmailHistory();
        }
        else if (messagingOption == MessagingOptions.SMS)
        {
            SmsHistoryService.ShowSmsHistory();
        }

    }
}
