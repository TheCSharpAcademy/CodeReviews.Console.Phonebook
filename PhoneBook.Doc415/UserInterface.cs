using PhoneBook.Doc415.Models;
using Spectre.Console;
using static PhoneBook.Doc415.Enums;
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
                                                                MainMenuSelections.UpdateContact,
                                                                MainMenuSelections.DeleteContact,
                                                                MainMenuSelections.SendEmail,
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
                case MainMenuSelections.DeleteContact:
                    DeleteContact();
                    break;
                case MainMenuSelections.UpdateContact:
                    UpdateContact();
                    break;
                case MainMenuSelections.SendEmail:
                    SendEmail();
                    break;
                case MainMenuSelections.Quit:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private void SendEmail()
    {
        var contact = SelectContact("send e-mail");
        EmailSender sender = new EmailSender();
        sender.SetUpSender();
        sender.SendEmail(contact);
    }
    private void AddContact()
    {
        string name = AnsiConsole.Ask<string>("Enter name: ");
        string title = AnsiConsole.Ask<string>("Enter title ", "");
        string phoneNumber = "";
        string email = "";
        do
        {
            phoneNumber = AnsiConsole.Ask<string>("\n\nSupported formats for phone numbers\n(+90 312 222 22 22)\n" +
                                                  "(312 222 22 22)\n" +
                                                  "(3122222222)\n\n" +
                                                  "Please enter phone number: ");
        } while (!Validators.isValidPhone(phoneNumber));

        do
        {
            email = AnsiConsole.Ask<string>("Enter e-mail adress (foo@foo.com) Press Enter to skip:  ", "");
            if (email == "")
                break;
        } while (!Validators.isValidEmail(email));

        DataAccess dataaccess = new();
        dataaccess.AddContact(name, email, phoneNumber, title);
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
        table.AddColumns("Name", "Title", "Phone number", "Email");
        foreach (var contact in contacts)
        {
            table.AddRow(contact.Name, contact.Title, contact.PhoneNumber, contact.Email);
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
        var phoneNumber = GetPhoneNumber(toUpdate.PhoneNumber);
        var email = GetEmail(toUpdate.Email);
        DataAccess dataAccess = new();
        dataAccess.UpdateContact(name, title, phoneNumber, email, toUpdate);
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
        } while (!Validators.isValidPhone(phoneNumber));
        return phoneNumber;
    }

    private string GetEmail(string email)
    {
        string newemail = "";
        do
        {
            newemail = AnsiConsole.Ask<string>("Enter e-mail adress (foo@foo.com) (press Enter to skip):  ", email);
        } while (!Validators.isValidEmail(newemail));

        return newemail;
    }


}
