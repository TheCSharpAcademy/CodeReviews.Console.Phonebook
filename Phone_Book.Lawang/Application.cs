using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Phone_Book.Lawang.Controller;
using Phone_Book.Lawang.Models;
using Phone_Book.Lawang.View;
using Spectre.Console;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Phone_Book.Lawang;

public class Application
{
    private readonly ContactController _contactController;
    private readonly CategoryController _categoryController;
    public Application(ContactController contactController, CategoryController categoryController)
    {
        _contactController = contactController;
        _categoryController = categoryController;
    }

    public void Build()
    {
        bool exitApp = false;

        var userInput = new UserInput();
        while (!exitApp)
        {
            Visual.ShowTitle("Phone Book");

            var operation = userInput.ChooseOperation();

            switch (operation.Value)
            {
                case 1:
                    var categories = _categoryController.GetCategories();

                    if (categories == null) break;

                    var listOfCategoryName = categories.Select(c => c.CategoryName).ToList();
                    listOfCategoryName.Add("Create New Category");
                    listOfCategoryName.Add("Exit");

                    var category = userInput.GetSelection(listOfCategoryName, "[green bold]Choose the category for contact: [/]", "What category do you want to choose? ");
                    if (category.Equals("Exit")) break;
                    int categoryId;
                    if (category.Equals("Create New Category"))
                    {

                        string newCategoryName = userInput.CreateCategory();
                        while (listOfCategoryName.Contains(newCategoryName))
                        {
                            AnsiConsole.MarkupLine($"[red bold]{newCategoryName} already exist, Try again! [/]");
                            newCategoryName = userInput.CreateCategory();
                        }

                        var newCategory = new Category() { CategoryName = newCategoryName };
                        categoryId = _categoryController.CreateCategory(newCategory).Id;

                    }
                    else
                    {
                        categoryId = categories.First(c => c.CategoryName == category).Id;
                    }


                    var contact = userInput.CreateContact();
                    if (contact == null) break;


                    contact.CategoryID = categoryId;

                    _contactController.CreateContact(contact);
                    break;
                case 2:
                    var contactList = ShowContactTable(userInput);
                    if (contactList != null)
                    {
                        AnsiConsole.MarkupLine("[grey bold](press 'Enter' to continue.)[/]");
                        Console.ReadLine();
                    }
                    break;
                case 3:
                    var listOfContact = _contactController.GetAllContacts();
                    contact = UpdateContact(userInput, listOfContact);

                    if (contact == null) break;

                    _contactController.UpdateContact(contact);
                    break;
                case 4:
                    listOfContact = _contactController.GetAllContacts();
                    contact = DeleteContact(userInput, listOfContact);

                    if (contact == null) break;

                    _contactController.DeleteContact(contact);
                    break;

                case 5:
                    listOfContact = ShowContactTable(userInput);
                    if (listOfContact == null) break;
                    if (listOfContact.Count() > 0)
                    {
                        int id = userInput.ChooseId(listOfContact);
                        if (id == 0) break;
                        contact = listOfContact.First(c => c.Id == id);
                        try
                        {

                            SendEmail(contact);
                            AnsiConsole.MarkupLine("[green bold]\nMessage was successfully Sent!\n[grey](Press 'Enter' to continue)[/][/]");
                            Console.ReadLine();
                        }
                        catch (Exception ex)
                        {
                            AnsiConsole.MarkupLine($"[red bold]{ex.Message}[/]");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.ReadLine();
                    }
                    break;
                case 6:
                    listOfContact = ShowContactTable(userInput);
                    if (listOfContact == null) break;
                    if (listOfContact.Count() > 0)
                    {
                        int id = userInput.ChooseId(listOfContact);
                        if (id == 0) break;
                        contact = listOfContact.First(c => c.Id == id);

                        SendSMS(contact);
                    }
                    else
                    {
                        Console.ReadLine();
                    }
                    break;
                case 0:
                    exitApp = true;
                    break;
            }
            Console.Clear();
        }

        static Contact? DeleteContact(UserInput userInput, IEnumerable<Contact> listOfContact)
        {

            Visual.ShowOperationTitle("[aqua bold]DELETE CONTACT[/]");
            Visual.ShowTable(listOfContact);
            if (listOfContact.Count() == 0)
            {
                AnsiConsole.MarkupLine("[grey bold](press 'Enter' to continue.)[/]");
                Console.ReadLine();
                return null;
            }

            int id = userInput.ChooseId(listOfContact);

            if (id == 0) return null;
            return listOfContact.First(x => x.Id == id);

        }

    }
    private IEnumerable<Contact>? ShowContactTable(UserInput userInput)
    {
        var categories = _categoryController.GetCategories();
        if (categories == null) return null;
        var listOfCategoryName = categories.Select(c => c.CategoryName).ToList();

        listOfCategoryName.Add("Show All");
        listOfCategoryName.Add("Exit");

        var category = userInput.GetSelection(listOfCategoryName, "[green bold]Show Contacts According to Categories: [/]", "What category do you want to choose? ");
        if (category.Equals("Exit")) return null;

        var listOfContact = _contactController.GetAllContacts();

        if (!category.Equals("Show All"))
        {
            var categoryId = categories.First(c => c.CategoryName == category).Id;
            listOfContact = listOfContact.Where(c => c.CategoryID == categoryId);
        }

        Visual.ShowTable(listOfContact);
        return listOfContact;
    }
    private Contact? UpdateContact(UserInput userInput, IEnumerable<Contact> listOfContact)
    {
        Visual.ShowOperationTitle("[aqua bold]UPDATE CONTACT[/]");
        Visual.ShowTable(listOfContact);

        if (listOfContact.Count() == 0)
        {
            AnsiConsole.MarkupLine("[grey bold](press 'Enter' to continue.)[/]");
            Console.ReadLine();
            return null;
        }

        int id = userInput.ChooseId(listOfContact);

        if (id == 0) return null;
        var updatedContact = listOfContact.First(x => x.Id == id);

        Console.Clear();
        var updateOption = userInput.ChooseUpdateOption();
        Console.Clear();

        updatedContact = userInput.UpdateContact(updatedContact, updateOption);

        if (updatedContact == null) return null;

        var option = userInput.GetSelection(new List<string> { "Yes", "No" }, "Do you want to update Category of the Contact", "");

        if (option.Equals("Yes"))
        {
            var categories = _categoryController.GetCategories();
            var listOfCategoryName = categories.Select(c => c.CategoryName).ToList();
            listOfCategoryName.Add("Create New Category");
            var category = userInput.GetSelection(listOfCategoryName, "[green bold]Choose the category for updated contact: [/]", "What category do you want to choose? ");

            int categoryId;

            if (category.Equals("Create New Category"))
            {

                string newCategoryName = userInput.CreateCategory();
                while (listOfCategoryName.Contains(newCategoryName))
                {
                    AnsiConsole.MarkupLine($"[red bold]{newCategoryName} already exist, Try again! [/]");
                    newCategoryName = userInput.CreateCategory();
                }

                var newCategory = new Category() { CategoryName = newCategoryName };
                categoryId = _categoryController.CreateCategory(newCategory).Id;

                updatedContact.CategoryID = categoryId;

            }
            else
            {
                updatedContact.CategoryID = categories.First(c => c.CategoryName == category).Id;
            }
        }

        return updatedContact;

    }

    private void SendEmail(Contact contact)
    {

        var subject = AnsiConsole.Ask<string>("Enter the subject of the email: ");
        var body = AnsiConsole.Ask<string>("Enter the body of the Email: ");

        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Application>().Build();
        var fromAddress = new MailAddress(config["Sender-Email"] ?? "", config["Name"]);
        var toAddress = new MailAddress(contact.Email ?? "");

        var fromPassword = config["App-Password"];

        var smtp = new SmtpClient()
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };

        using var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        };

        AnsiConsole.Status().Start($"[green bold] Sending Email to {contact.Name} ...[/]", ctx =>
        {
            smtp.Send(message);
        });


    }

    private void SendSMS(Contact contact)
    {
        IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Application>().Build();
        var messageBody = AnsiConsole.Ask<string>("[yellow bold]Write Your Message: [/]");

        var twilioSID = config["TWILIO_SID"];
        var twilioAUTH = config["TWILIO_AUTH"];
        var twilioPhoneNumber = config["TWILIO_PHONE_NUMBER"];

        TwilioClient.Init(twilioSID, twilioAUTH);

        var senderNumber = new PhoneNumber(twilioPhoneNumber);
        var receiverNumber = new PhoneNumber(contact.PhoneNumber);



        try
        {
            var message = MessageResource.Create(
                body: messageBody,
                from: senderNumber,
                to: receiverNumber
            );

            AnsiConsole.MarkupLine($"\n[green bold]Message Sent with SID: {message.Sid}[/]");
            var messageStatus = MessageResource.Fetch(pathSid: message.Sid);
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red bold]{ex.Message}[/]");
            Console.ReadLine();
        }
    }
}
