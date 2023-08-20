using Microsoft.EntityFrameworkCore;
using PhoneBookCarDioLogics.Models;
using Spectre.Console;
using System.Net.Mail;

namespace PhoneBookCarDioLogics.Controllers;

internal class ContactsController
{
    internal static void InsertContact()
    {
        using var context = new PhonebookAppDbContext();

        if (context.Categories.ToList().Count() > 0) //if there are categories the code below here runs
        {

            var contact = new Contact();
            contact.Name = AnsiConsole.Ask<string>("Contact's Name:");
            contact.PhoneNumber = AnsiConsole.Ask<string>("Contact's phone Number:");
            contact.CategoryID = CategoryController.GetCategoryOptionInput().CategoryId;
            contact.Email = AnsiConsole.Confirm("Add email to contact?") ? AnsiConsole.Ask<string>("Contact's email:") : contact.Email = "";

            if (contact.Email != "")
            {
                string emailDomain = contact.Email.Split('@')[1];
                if (emailDomain != "gmail.com" && emailDomain != "hotmail.com")
                {
                    contact.Email = "";
                    Console.WriteLine("Email domain not supported");
                    Console.ReadLine();
                }
            }

            context.Add(contact);
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("Can't create a contact! No categories exist yet!");
            Console.ReadLine();
        }
    }

    internal static List<Contact> ShowContacts()
    {
        using var context = new PhonebookAppDbContext();

        var contacts = context.Contacts
            .Include(x => x.Category)
            .ToList();

        UserInterface.ShowContacts(contacts);
        return contacts;
    }

    internal static void ChangeContact()
    {
        var contact = GetContactOptionInput();

        if (contact != null)
        {
            contact.Name = AnsiConsole.Confirm("Update name?") ? AnsiConsole.Ask<string>("Contact's new name:") : contact.Name;
            contact.PhoneNumber = AnsiConsole.Confirm("Update phone number?") ? AnsiConsole.Ask<string>("Contact's new phone number:") : contact.PhoneNumber;
            contact.Email = AnsiConsole.Confirm("Update email?") ? AnsiConsole.Ask<string>("Contact's new email:") : contact.Email;
            contact.Category = AnsiConsole.Confirm("Update category?") ? CategoryController.GetCategoryOptionInput() : contact.Category;

            using var db = new PhonebookAppDbContext();
            db.Update(contact);
            db.SaveChanges();
        }
        else 
        {
            Console.WriteLine("No contacts exist yet!");
            Console.ReadLine();
        }
    }

    internal static void RemoveContact()
    {
        var contact = GetContactOptionInput();
        
        if (contact != null)
        {
            using var db = new PhonebookAppDbContext();
            db.Remove(contact);
            db.SaveChanges();
        }
        else
        {
            Console.WriteLine("No contacts exist yet!");
            Console.ReadLine();
        }
    }

    private static Contact GetContactOptionInput()
    {
        List<Contact> contacts = ShowContacts();
        bool contactExists;

        if(contacts.Count > 0)
        {
            var contactsArray = contacts.Select(x => x.Name).ToArray();

            var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Choose contact")
                .AddChoices(contactsArray));
            var id = contacts.Single(x => x.Name == option).ContactId;
            var contact = GetContactByID(id);

            contactExists = true;

            return contact;
        }
        else
        {
            return null;
        }
    }

    private static Contact GetContactByID(int id)
    {
        using var context = new PhonebookAppDbContext();

        var contact = context.Contacts
            .Include(x => x.Category)
            .SingleOrDefault(x => x.ContactId == id);

        return contact;
    }

    internal static void PrepareAndSendEmail()
    {
        Console.WriteLine("Select who do you want to send an email to:");
        var contact = GetContactOptionInput();

        if(contact != null)
        {
            string emailRecipient = contact.Email;

            if (emailRecipient != "")
            {
                string emailSender = AnsiConsole.Ask<string>("Sender's email:");

                if(IsValidEmail(emailSender))
                {
                    string emailSenderPassword = AnsiConsole.Prompt(new TextPrompt<string>("Sender's email password:").Secret());

                    string emailDomain = emailSender.Split('@')[1];

                    SmtpClient smtpClient = EmailService.SetEmailClient(emailDomain, emailSender, emailSenderPassword);

                    //if the email domain is supported. The user is allowed to create and send the email.
                    if (emailDomain == "gmail.com" || emailDomain == "hotmail.com")
                    {
                        MailMessage mailMessage = new MailMessage(emailSender, emailRecipient)
                        {
                            Subject = AnsiConsole.Ask<string>("Subject of the email:"),
                            Body = AnsiConsole.Ask<string>("Write the content of the email:"),
                            IsBodyHtml = false, // Set to true if you want to use HTML in the email body
                        };

                        smtpClient.Send(mailMessage);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid sender email address format.");
                    Console.ReadLine();
                } 
            }
            else
            {
                Console.WriteLine("This contact does not have a valid email adress.");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("Can´t send email! No contacts exist yet!");
            Console.ReadLine();
        }
    }


    internal static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
