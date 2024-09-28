using Microsoft.Extensions.Configuration;
using Spectre.Console;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace PhoneBook
{
    public class PhonebookService
    {
        static IConfiguration configuration = ConfigurationHelper.BuildConfiguration();
        static string senderEmail = configuration["SENDER_EMAIL"];
        static string password = configuration["PASSWORD"];

        static string accountSid = configuration["Twilio_SID"];
        static string authToken = configuration["Twilio_Auth_Token"];
        static string senderNumber = configuration["SENDER_NUMBER"];
        static string countryCode = configuration["COUNTRY_CODE"];


        public static void PerformOperation(int opt)
        {
            Console.Clear();
            UserInput userInput = new();
            PhonebookRepositiory repo = new();
            Validation validation = new();

            int choice;

            switch (opt)
            {
                case 1:
                    Console.Write("Enter Contact Name: ");
                    string name = userInput.GetText();
                    Console.Write("Enter Contact Number (10 digits): ");
                    string number = userInput.GetNumber();
                    Console.Write("Enter Contact email: ");
                    string email = userInput.GetEmail();
                    string category = null;
                    Console.Write("Press (y/Y) to add to category?: ");
                    if(Console.ReadLine().Substring(0, 1).ToLower() == "y")
                    {
                        Console.Write("Enter category: ");
                        category = Console.ReadLine();

                    }
                    repo.AddContact(name, number, email, category);
                    break;

                case 2:
                    choice = userInput.ViewMenuChoice();
                    if (choice == 3) return;

                    switch (choice)
                    {
                        case 1:
                            repo.ViewContacts();
                            break;
                        case 2:
                            var categories = repo.GetCategories();
                            category = userInput.CategoryMenu(categories);
                            repo.ViewContacts(category);
                            break;
                    }
                    break;

                case 3:

                    choice = userInput.UpdateMenuChoice();

                    if (choice == 5) return;

                    repo.ViewContacts(); 

                    Console.Write("Enter Phone id which is to be updated: ");
                    int id = userInput.GetId();
                    if (!repo.CheckIdExists(id))
                    {
                        AnsiConsole.Markup("[red]Contact Id doesn't exist. Returning to Main Menu....[/]\n");
                        Thread.Sleep(1000);
                        Console.Clear();
                        return;
                    }

                    switch (choice)
                    {
                        case 1:
                            string updateColumn = "Name";
                            repo.GetCurrentValue(id, updateColumn);

                            Console.Write("Enter new contact name: ");
                            name = userInput.GetText();
                            repo.UpdateContacts(id, name, updateColumn);
                            break;

                        case 2:
                            updateColumn = "PhoneNumber";
                            repo.GetCurrentValue(id, updateColumn);

                            Console.Write("Enter new contact number: ");
                            number = userInput.GetNumber();
                            
                            repo.UpdateContacts(id, number, updateColumn);
                            break;

                        case 3:
                            updateColumn = "Email";
                            repo.GetCurrentValue(id, updateColumn);

                            Console.Write("Enter new contact email: ");
                            email = userInput.GetEmail() ;
                            repo.UpdateContacts(id, email, updateColumn);
                            break;

                        case 4:
                            updateColumn = "Category";
                            repo.GetCurrentValue(id, updateColumn);

                            Console.Write("Enter new contact category: ");
                            category = Console.ReadLine();
                            validation.IsValidName(category);
                            repo.UpdateContacts(id, category, updateColumn);
                            break;
                    }
                    break;

                case 4:
                    repo.ViewContacts();
                    Console.Write("\nEnter contact id to be deleted: ");
                    id = userInput.GetId();

                    if (!repo.CheckIdExists(id))
                    {
                        AnsiConsole.Markup("[red]Contact Id doesn't exist. Returning to Main Menu....[/]\n");
                        Thread.Sleep(1000);
                        Console.Clear();
                        return;
                    }

                    repo.DeleteContact(id);
                    break;

                case 5:
                    repo.ViewContacts();
                    Console.Write("Enter contact id to whom you want to send email: ");
                    id = userInput.GetId();

                    if (!repo.CheckIdExists(id))
                    {
                        AnsiConsole.Markup("[red]Contact Id doesn't exist. Returning to Main Menu....[/]\n");
                        Thread.Sleep(1000);
                        Console.Clear();
                        return;
                    }

                    string receiverEmail = repo.GetEmail(id);
                    Console.WriteLine("\nEnter subject: ");
                    string subject = Console.ReadLine();

                    string body = userInput.GetMessage();
                    SendMail(receiverEmail, subject, body);
                    break;

                case 6:
                    repo.ViewContacts();

                    Console.Write("Enter contact id to whom you want to send sms: ");
                    id = userInput.GetId();

                    if (!repo.CheckIdExists(id))
                    {
                        AnsiConsole.Markup("[red]Contact Id doesn't exist. Returning to Main Menu....[/]\n");
                        Thread.Sleep(1000);
                        Console.Clear();
                        return;
                    }

                    string receiverNumber = repo.GetNumber(id);
         
                    string message = userInput.GetMessage();
                    SendSMS(receiverNumber, message);

                    break;
            }

            AnsiConsole.Markup("[blue]Press any key to continue[/]");
            Console.ReadLine();
            Console.Clear() ;
            return;
        }

        private static void SendSMS(string receiverNumber, string message)
        {
            try
            {


                TwilioClient.Init(accountSid, authToken);

         
                var sms = MessageResource.Create(
                        body: message,
                        from: new Twilio.Types.PhoneNumber(senderNumber),
                        to: new Twilio.Types.PhoneNumber(countryCode+receiverNumber));

                AnsiConsole.Markup($"\n[blue]SMS sent Successfully[/]\n\n");
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Error sending sms: {ex.Message}[/]\n\n");
            }
        }

        private static void SendMail(string receiverEmail, string? subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail);
                mail.To.Add(receiverEmail);
                mail.Subject = subject;
                mail.Body = body;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new NetworkCredential(senderEmail, password);
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);

                AnsiConsole.Markup("\n[blue]Mail sent successfully[/]\n\n");
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Error sending mail: {ex.Message}[/]\n\n");
            }
        }
    }
}
