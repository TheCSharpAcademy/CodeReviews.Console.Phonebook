using System.Net.Mail;
using System.Net;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using Phonebook.Models;
using Phonebook.Helper;
using Phonebook.Queries;

namespace Phonebook;

internal class Application
{
    internal Application()
    {
        DotNetEnv.Env.TraversePath().Load();
    }
    internal void MainMenu()
    {
        bool runApp = true;
        while (runApp)
        {
            Console.Clear();
            string option = Display.GetSelection("Hi! What do you wish to do?", new List<string> { "Create Contact", "Edit Contact", "Delete Contact", "View Contacts", "Sms Contact", "Email Contact", "Quit" });
            switch (option)
            {
                case "Create Contact":
                    CreateContact();
                    break;
                case "Edit Contact":
                    EditContacts();
                    break;
                case "Delete Contact":
                    DeleteContact();
                    break;
                case "View Contacts":
                    ViewContacts();
                    break;
                case "Sms Contact":
                    SendSms();
                    break;
                case "Email Contact":
                    SendEmailToContact();
                    break;
                case "Quit":
                    Console.WriteLine("Goodbye");
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }

    private void SendEmailToContact()
    {
        List<Contact> contacts = DbQueries.GetAllContacts();
        Display.ShowContacts(["Id", "Name", "Phone-no", "Emailid"], contacts);
        string? Id;
        do
        {
            Console.WriteLine("Enter valid Id of contact to send a message:");
            Id = Console.ReadLine();
        } while (!InputValidator.IsGivenInputInteger(Id));
        if (DbQueries.IsGivenIdPresent(int.Parse(Id)))
        {
            Console.WriteLine("Enter the subject of the email:");
            string subject = Console.ReadLine();
            Console.WriteLine("Enter the body of the email:");
            string body = Console.ReadLine();
            try
            {
                SendEmail(DbQueries.GetEmailId(int.Parse(Id)), subject, body);
                Console.WriteLine("Your email was sent successfully.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("The given id was not found in the database");
            Console.ReadLine();
        }
    }

    private void SendEmail(string toEmail, string? subject, string? body)
    {
        var fromAddress = new MailAddress(DotNetEnv.Env.GetString("EmailId"), DotNetEnv.Env.GetString("Name"));
        var toAddress = new MailAddress(toEmail);
        string fromPassword = DotNetEnv.Env.GetString("AppPassword");
        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };
        using (var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        })
        {
            smtp.Send(message);
        }
    }

    private void SendSms()
    {
        List<Contact> contacts = DbQueries.GetAllContacts();
        Display.ShowContacts(["Id", "Name", "Phone-no", "Emailid"], contacts);
        string? Id;
        do
        {
            Console.WriteLine("Enter valid Id of contact to send a message:");
            Id = Console.ReadLine();
        } while (!InputValidator.IsGivenInputInteger(Id));
        if (DbQueries.IsGivenIdPresent(int.Parse(Id)))
        {
            string? twilioAccountSid = DotNetEnv.Env.GetString("TWILIO_ACCOUNT_SID");
            string? twilioAccountToken = DotNetEnv.Env.GetString("TWILIO_AUTH_TOKEN");
            string? twilioPhoneNumber = DotNetEnv.Env.GetString("TWILIO_PHONE_NUMBER");
            TwilioClient.Init(twilioAccountSid, twilioAccountToken);
            var fromPhoneNumber = new PhoneNumber(twilioPhoneNumber);
            var toPhoneNumber = new PhoneNumber(DbQueries.GetPhoneNo(int.Parse(Id)));
            Console.WriteLine("Enter the message you want to send:");
            var messageBody = Console.ReadLine();
            try
            {
                var message = MessageResource.Create(
                    body: messageBody,
                    from: fromPhoneNumber,
                    to: toPhoneNumber
                );
                Console.WriteLine($"Message sent with SID: {message.Sid}");
                var messageStatus = MessageResource.Fetch(pathSid: message.Sid);
                Console.WriteLine($"Message status: {messageStatus.Status}");
                Console.WriteLine("Press any key to continue....");
                Console.ReadLine();
            }
            catch(Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue....");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("The given id was not found in the database");
            Console.ReadLine();
        }
    }

    private void ViewContacts()
    {
        string option = Display.GetSelection("Hi! Do you wish to see all contacts or view a specific category ?", new List<string> { "All Contacts", "View Specific Category" , "Go to Main Menu" });
        if(option == "View Specific Category")
        {
            List<string> categories = DbQueries.GetAllCategories();
            string selectedCategory = Display.GetSelection("Please select from the list of categories", categories);
            List<Contact> contacts = DbQueries.GetContactsSpecificCategory(selectedCategory);
            Console.WriteLine("Following are the contacts in the {0} category", selectedCategory);
            Display.ShowContacts(["Id","Name", "Phone-no", "Emailid"], contacts);
            Console.ReadLine();
        }
        else if(option == "All Contacts")
        {
            List<Contact> contacts = DbQueries.GetAllContacts();
            Display.ShowContacts(["Id", "Name", "Phone-no", "Emailid"], contacts);
            Console.ReadLine();
        }
       
    }

    private void DeleteContact()
    {
        if (DbQueries.AreContactsPresent())
        {
            List<Contact> contacts = DbQueries.GetAllContacts();
            Display.ShowContacts(["Id", "Name", "Phone-no", "Emailid"], contacts);
            string? Id;
            do
            {
                Console.WriteLine("Enter the contact id to be deleted:");
                Id = Console.ReadLine();
            } while (!InputValidator.IsGivenInputInteger(Id));
            if (DbQueries.IsGivenIdPresent(int.Parse(Id)))
            {
                DbQueries.DeleteContact(int.Parse(Id));
                Console.WriteLine("Contact deleted successfully! Press any key to continue....");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("The given id was not found in the database");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("No contacts present in the database. Nothing to delete");
            Console.ReadLine();
        } 
    }

    private void EditContacts()
    {
        DbQueries.CreateDefaultCategory();
        if (DbQueries.AreContactsPresent())
        {
            Console.WriteLine("---------Following contacts are present in the database-----------");
            List<Contact> contacts = DbQueries.GetAllContacts();
            Display.ShowContacts(["Id", "Name", "Phone-no", "Emailid"], contacts);
            string ?Id;
            do
            {
                Console.WriteLine("Enter valid Id of contact to edit:");
                Id = Console.ReadLine();
            } while (!InputValidator.IsGivenInputInteger(Id));
            if (DbQueries.IsGivenIdPresent(int.Parse(Id)))
            {
                Contact contact = new Contact();
                contact.Id = int.Parse(Id);
                Console.Write("Enter Name: ");
                contact.ContactName = Console.ReadLine();
                do
                {
                    Console.Write("Enter Email: ");
                    contact.ContactEmailid = Console.ReadLine();
                } while (!InputValidator.IsValidEmail(contact.ContactEmailid));
                do
                {
                    Console.Write("Enter Phone Number with country code: ");
                    contact.ContactPhoneno = Console.ReadLine();
                } while (!InputValidator.IsValidPhoneNumber(contact.ContactPhoneno));
                string option = Display.GetSelection("Do you wish to register this contact under a specific category?", new List<string> { "Yes", "No" });
                if (option == "Yes")
                {
                    List<string> categories = DbQueries.GetAllCategories();
                    categories.Add("Create a new category");
                    string selectedCategory = Display.GetSelection("Please select from the list of categories", categories);
                    if (selectedCategory == "Create a new category")
                    {
                        string category;
                        do
                        {
                            Console.Write("Enter a new category name: ");
                            category = Console.ReadLine();
                        } while (DbQueries.IsCategoryAlreadyPresent(category));
                        DbQueries.CreateCategory(category);
                        contact.CategoryId = DbQueries.GetCategoryId(category);
                    }
                    else
                        contact.CategoryId = DbQueries.GetCategoryId(selectedCategory);
                }
                else
                    contact.CategoryId = DbQueries.GetCategoryId("Default");
                DbQueries.EditDetails(contact);
                Console.WriteLine("Contact updated successfully! Press any key to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("The given id was not found in the database");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("No contacts present to edit. Kindly insert some contacts.");
            Console.ReadLine();
        }
    }

    private void CreateContact()
    {
        DbQueries.CreateDefaultCategory();
        Contact contact = new Contact();
        Console.Write("Enter Name: ");
        contact.ContactName = Console.ReadLine();
        do
        {
            Console.Write("Enter Email: ");
            contact.ContactEmailid = Console.ReadLine();
        } while (!InputValidator.IsValidEmail(contact.ContactEmailid));
        do
        {
            Console.Write("Enter Phone Number with country code: ");
            contact.ContactPhoneno = Console.ReadLine();
        } while (!InputValidator.IsValidPhoneNumber(contact.ContactPhoneno));
        string option = Display.GetSelection("Do you wish to register this contact under a specific category?", new List<string> {"Yes", "No"});
        if (option == "Yes")
        {
            List<string> categories = DbQueries.GetAllCategories();
            categories.Add("Create a new category");
            string selectedCategory = Display.GetSelection("Please select from the list of categories", categories);
            if(selectedCategory == "Create a new category")
            {
                string category;
                do
                {
                    Console.Write("Enter a new category name: ");
                    category = Console.ReadLine();
                } while (DbQueries.IsCategoryAlreadyPresent(category));
                DbQueries.CreateCategory(category);
                contact.CategoryId = DbQueries.GetCategoryId(category);
            }
            else
                contact.CategoryId = DbQueries.GetCategoryId(selectedCategory);
        }
        else
        {
            contact.CategoryId = DbQueries.GetCategoryId("Default");
        }
        DbQueries.SaveContact(contact);
        Console.WriteLine("Contact added successfully! Press any key to continue....");
        Console.ReadLine();
    }
}