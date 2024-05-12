using System.Net;
using System.Net.Mail;
using Spectre.Console;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using static PhoneBook.Enums;

namespace PhoneBook;

internal static class Service
{
    internal static void ViewContacts()
    {
        var option = AnsiConsole.Prompt(new SelectionPrompt<Contacts>()
        .Title("Choose: ")
        .AddChoices(
            Contacts.ViewAllContacts,
            Contacts.FilterByCategory
        ));
        if (option == Contacts.ViewAllContacts)
        {
            var contacts = Controller.Read();
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts available.");
                Console.WriteLine("Press enter to go back to main menu.");
                Console.ReadLine();
                UserInterface.MainMenu();
            }
            UserInterface.ShowContacts(contacts);
        }
        else if (option == Contacts.FilterByCategory)
        {
            var contacts = Controller.Read();
            var categories = contacts.Select(c => c.Category).ToArray();
            var category = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose category: ")
            .AddChoices(categories));
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts available.");
                Console.WriteLine("Press enter to go back to main menu.");
                Console.ReadLine();
                UserInterface.MainMenu();
            }
            var filteredContacts = contacts.Where(p => p.Category == category).ToList();

            UserInterface.ShowContacts(filteredContacts);
        }
    }
    internal static void InsertContact()
    {
        var contacts = Controller.Read();
        var contact = new Contact();
        contact.Name = AnsiConsole.Ask<string>("Contact's name: ");
        contact.PhoneNumber = AnsiConsole.Ask<string>("Contact's phone number(format: +###########):");
        while (!Validation.ValidateNumber(contact.PhoneNumber))
        {
            Console.WriteLine("Invalid Format. Use the following format (+###########)");
            contact.PhoneNumber = AnsiConsole.Ask<string>("Contact's phone number:");
        }
        contact.Email = AnsiConsole.Ask<string>("Contact's email: ");
        while (!Validation.ValidateEmail(contact.Email))
        {
            Console.WriteLine("Invalid Email. Use the following format (example@example.com)");
            contact.Email = AnsiConsole.Ask<string>("Contact's email: ");
        }
        contact.Category = AnsiConsole.Ask<string>("Contact Category: ");
        while (Validation.ValidateCategory(contact.Category))
        {
            Console.WriteLine("Category can't be null or a number");
            contact.Category = AnsiConsole.Ask<string>("Contact Category: ");
        }

        Controller.Insert(contact);
    }
    internal static void DeleteContact()
    {
        var contact = GetContactInput();
        Controller.Delete(contact);
    }
    internal static void UpdateContact()
    {
        var contact = GetContactInput();
        contact.Name = AnsiConsole.Confirm("Update name?")
        ? AnsiConsole.Ask<string>("Contact's new name: ")
        : contact.Name;

        contact.PhoneNumber = AnsiConsole.Confirm("Update Phone Number?")
        ? AnsiConsole.Ask<string>("Contact's new phone number: ")
        : contact.PhoneNumber;

        contact.Email = AnsiConsole.Confirm("Update email?")
        ? AnsiConsole.Ask<string>("Contact's new email: ")
        : contact.Email;

        contact.Category = AnsiConsole.Confirm("Update Category?")
        ? AnsiConsole.Ask<string>("Contact Category: ")
        : contact.Category;

        Controller.Update(contact);
    }
    internal static Contact GetContactInput()
    {
        var contacts = Controller.Read();

        var contactArray = contacts.Select(p => p.Name).ToArray();
        if (contactArray.Length == 0)
        {
            Console.WriteLine("No contacts available.");
            Console.WriteLine("Press enter to go back to main menu.");
            Console.ReadLine();
            UserInterface.MainMenu();
        }

        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title("Choose Contact")
        .AddChoices(contactArray));

        var id = contacts.Single(p => p.Name == option).Id;
        var contact = Controller.GetContact(id);

        return contact;
    }
    internal static void SendEmail()
    {
        try
        {
            string senderEmail = AnsiConsole.Ask<string>("Your email: ");
            Console.WriteLine("If you have 2 factor authentication you need an app password");
            string senderPassword = AnsiConsole.Ask<string>("Password: ");

            var contacts = Controller.Read();
            var emails = contacts.Select(p => p.Email).ToArray();
            var recipientEmail = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose email")
            .AddChoices(emails));

            MailMessage message = new MailMessage(senderEmail, recipientEmail);

            message.Subject = AnsiConsole.Ask<string>("Mail subject: ");
            message.Body = AnsiConsole.Ask<string>("Message: ");
            SmtpClient[] smtpClients =
            {
                new SmtpClient("smtp.gmail.com"),
                new SmtpClient("smtp.office365.com"),
                new SmtpClient("smtp.mail.yahoo.com"),
                new SmtpClient("smtp.zoho.com"),
                new SmtpClient("smtp.fastmail.com"),
                new SmtpClient("smtp.gmx.com"),
                new SmtpClient("smtp.mail.com")
            };
            bool sentMail = false;
            foreach (var smtpClient in smtpClients)
            {
                int startingIndex = senderEmail.IndexOf('@') + 1;
                int indexOfDot = senderEmail.IndexOf('.');
                int length = indexOfDot - startingIndex;
                string mailClient = senderEmail.Substring(startingIndex, length);
                if (mailClient == "hotmail") mailClient = "office365";
                if (smtpClient.Host.Contains(mailClient))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    smtpClient.EnableSsl = true;

                    smtpClient.Send(message);
                    Console.WriteLine("Email sent successfully!");
                    sentMail = true;
                }
            }

            if (sentMail == false)
            {
                Console.WriteLine("Email failed to send!");
                Console.WriteLine("Your domain might not be in our list. Try again using a different email.");
                Console.WriteLine("Press enter to go back to main menu.");
                Console.ReadLine();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
    internal static void SendSMS()
    {
        Console.WriteLine("WARNING! You can only send SMS if you have a Twilio account created.");
        Console.WriteLine("Do you have a twilio account? (yes/no)");
        var option = Console.ReadLine();
        if (option == "yes")
        {
            string accountSid = AnsiConsole.Ask<string>("Your Twilio Account SID: ");
            string authToken = AnsiConsole.Ask<string>("Your Twilio Account Token: ");
            TwilioClient.Init(accountSid, authToken);

            string fromPhoneNumber = AnsiConsole.Ask<string>("Your Twilio Phone Number: ");
            string toPhoneNumber = AnsiConsole.Ask<string>("SMS Recipient Phone Number: ");
            string messageBody = AnsiConsole.Ask<string>("Message: ");

            try
            {
                var message = MessageResource.Create(
                    body: messageBody,
                    from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
                    to: new Twilio.Types.PhoneNumber(toPhoneNumber)
                );
                Console.WriteLine($"SMS sent successfully. Message SID: {message.Sid}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send SMS: {ex.Message}");
            }
        }
        else if (option == "no")
        {
            Console.WriteLine("Go to this website https://www.twilio.com/en-us and sign up to be able to send SMS.");
            Console.WriteLine("Press enter to go back to main menu.");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("That is not an option!");
            Console.WriteLine("Press enter to go back to main menu");
            Console.ReadLine();
        }
    }
}