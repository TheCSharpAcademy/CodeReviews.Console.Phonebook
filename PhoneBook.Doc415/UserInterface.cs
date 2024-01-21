using PhoneBook.Doc415.Models;
using PhoneNumbers;
using Spectre.Console;
using static PhoneBook.Doc415.Enums;
using static System.Collections.Specialized.BitVector32;
namespace PhoneBook.Doc415;

internal class UserInterface
{
    public void MainMenu()
    {
        while (true)
        {
            AnsiConsole.Write(new FigletText("Phone Book").Color(Color.LightSalmon3_1).Centered());
            var selection = AnsiConsole.Prompt(new SelectionPrompt<MainMenuSelections>()
                                                    .Title("Main Menu")
                                                    .AddChoices(MainMenuSelections.AddContact,
                                                                MainMenuSelections.ViewContacts,
                                                                MainMenuSelections.ViewByCategory,
                                                                MainMenuSelections.UpdateContact,
                                                                MainMenuSelections.DeleteContact,
                                                                MainMenuSelections.SendEmail,
                                                                MainMenuSelections.SendSMS,
                                                                MainMenuSelections.Quit
                                                   ));
            switch (selection)
            {
                case MainMenuSelections.AddContact:
                    AddContact();
                    break;
                case MainMenuSelections.ViewContacts:
                    ViewContacts();
                    break;
                case MainMenuSelections.ViewByCategory:
                    ViewByCategory();
                    break;
                case MainMenuSelections.DeleteContact:
                    DeleteContact();
                    break;
                case MainMenuSelections.UpdateContact:
                    UpdateContact();
                    break;
                case MainMenuSelections.SendEmail:
                    SendEmail();
                    break;
                case MainMenuSelections.SendSMS:
                    SendSMS();
                    break;
                case MainMenuSelections.Quit:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private void ViewByCategory()
    {
        DataAccess dataaccess = new();
        var categories=dataaccess.GetCategories();
        string selectedCategory =  AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title($"Choose category")
            .AddChoices(categories)
            );
        var contacts = dataaccess.GetContactsByCategory(selectedCategory);
        var table = new Table();
        table.AddColumns("Name", "Title","Category", "Phone number", "Email");
        foreach (var contact in contacts)
        {
            table.AddRow(contact.Name, contact.Title,contact.Category, contact.PhoneNumber, contact.Email);
        }
        table.Centered();
        AnsiConsole.Write(table);
        EndOperation("Press Enter to continue...");
    }

    private void SendSMS()
    {
        string number = "";
        if (!SMShandler.isFunctional())
        {
            Console.WriteLine("SMS sender has not setup.");
            SetUpEmail.SetUp();
            Console.Clear();
        }
        if (!SMShandler.isFunctional())
            return;
        else
        {
            var contact = SelectContact("send SMS");
            Console.WriteLine($"Sending SMS to {contact.Name} :");
            string message = AnsiConsole.Ask<string>("SMS message: ");
            if (!contact.PhoneNumber.StartsWith("+"))
            {
                var phoneUtil = PhoneNumberUtil.GetInstance(); ;
                string countryCode = phoneUtil.GetCountryCodeForRegion(CountrySelection.CountryCode).ToString();
                number="+"+countryCode+contact.PhoneNumber;
            }

            SMShandler.SendSMS(number, message);
        }
    }

    private void SendEmail()
    {
        if (!SetUpEmail.isEmailFunctional())
        {
            Console.WriteLine("Email sender has not setup.");
            SetUpEmail.SetUp();
            Console.Clear();
        }
        if (!SetUpEmail.isEmailFunctional())
            return;
        else
        {
            var contact = SelectContact("send e-mail");
            EmailSender sender = new EmailSender();
            Console.WriteLine($"Sending e-mail to {contact.Email} :");
            string subject = AnsiConsole.Ask<string>("Subject: ", "");
            string body = AnsiConsole.Ask<string>("Mail: "); sender.SendEmail(contact.Email, subject, body);
        }
    }
    private void AddContact()
    {
        string name = AnsiConsole.Ask<string>("Enter name: ");
        string title = AnsiConsole.Ask<string>("Enter title ", "");
        string category = AnsiConsole.Ask<string>("Enter category ", "");
        string phoneNumber = "";
        string email = "";
        do
        {
            phoneNumber = AnsiConsole.Ask<string>("\n\nSupported formats for phone numbers\n(+90 312 222 22 22)\n" +
                                                  "(312 222 22 22)\n" +
                                                  "(3122222222)\n\n" +
                                                  "Please enter phone number: ");
        } while (!Validators.IsValidPhone(phoneNumber));

        do
        {
            email = AnsiConsole.Ask<string>("Enter e-mail adress (foo@foo.com) Press Enter to skip:  ", "");
            if (email == "")
                break;
        } while (!Validators.IsValidEmail(email));

        DataAccess dataaccess = new();
        dataaccess.AddContact(name, email, phoneNumber, title, category);
        EndOperation("Contact recorded. Press Enter to continue...");
    }

    private Contact SelectContact(string title)
    {
        DataAccess dataaccess = new();
        var contacts = dataaccess.GetContacts();
        List<string> contactList = new();
        foreach (var contact in contacts)
        {
            string combined = contact.ContactID.ToString().PadRight(10) + " " + contact.Name.PadRight(30) + " " + contact.Title.PadRight(10) + " " + contact.PhoneNumber.PadRight(20) + " " + contact.Email;
            contactList.Add(combined);
        }
        var selection = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title($"Choose contact to {title}")
            .AddChoices(contactList)
            );
        var index = contactList.IndexOf(selection);
        Contact selected = contacts[index];
        return selected;
    }

    private void ViewContacts()
    {
        DataAccess dataaccess = new();
        var contacts = dataaccess.GetContacts();
        var table = new Table();
        table.AddColumns("Name", "Title", "Category","Phone number", "Email");
        foreach (var contact in contacts)
        {
            table.AddRow(contact.Name, contact.Title, contact.Category, contact.PhoneNumber, contact.Email);
        }
        table.Centered();
        AnsiConsole.Write(table);
        EndOperation("Press Enter to continue...");
    }

    private void DeleteContact()
    {
        Contact toDelete = SelectContact("delete");
        if (AnsiConsole.Confirm($"This will delete {toDelete.Title} {toDelete.Name} from Phone Book, are you sure", false))
        {
            DataAccess dataaccess = new();
            dataaccess.DeleteContact(toDelete);
            EndOperation("Contact deleted. Press Enter to continue...");
        }
        else
            EndOperation("Press enter to return main menu");
    }

    private void UpdateContact()
    {
        Contact toUpdate = SelectContact("update");
        var name = AnsiConsole.Ask<string>("Enter new name (press enter to skip): ", toUpdate.Name);
        var title = AnsiConsole.Ask<string>("Enter new title (press enter to skip): ", toUpdate.Title);
        var category = AnsiConsole.Ask<string>("Enter new category (press enter to skip): ", toUpdate.Category);
        var phoneNumber = GetPhoneNumber(toUpdate.PhoneNumber);
        var email = GetEmail(toUpdate.Email);
        DataAccess dataAccess = new();
        dataAccess.UpdateContact(name, title, phoneNumber, email, category, toUpdate);
        EndOperation("Update completed. Press Enter to continue...");
    }


    private void EndOperation(string message)
    {
        AnsiConsole.Markup($"[lightpink3]{message}[/]");
        Console.ReadLine();
        Console.Clear();
    }


    private string GetPhoneNumber(string oldphone)
    {
        string phoneNumber = "";
        do
        {
            phoneNumber = AnsiConsole.Ask<string>("\n\nSupported formats for phone numbers\n(+90 312 222 22 22)\n" +
                                                    "(312 222 22 22)\n" +
                                                    "(3122222222)\n\n" +
                                                    "Please enter phone number (press enter to skip): ", oldphone);
        } while (!Validators.IsValidPhone(phoneNumber));
        return phoneNumber;
    }

    private string GetEmail(string email)
    {
        string newemail = "";
        do
        {
            newemail = AnsiConsole.Ask<string>("Enter e-mail adress (foo@foo.com) (press Enter to skip):  ", email);
        } while (!Validators.IsValidEmail(newemail));

        return newemail;
    }


}
