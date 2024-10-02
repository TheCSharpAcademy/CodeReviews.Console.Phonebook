using MimeKit;
using Phonebook.Library.Data;
using Phonebook.Library.Models;
using Spectre.Console;

namespace Phonebook.Library;
public class UserInput
{
    static readonly List<string> menuOptions = ["Add contacts", "Delete contacts", "Edit contact", "View contacts", "Send email", "Exit"];

    public static string ShowMenu()
    {
        Console.Clear();
        return AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose an option:")
            .AddChoices(menuOptions));
    }

    public static bool SwitchMenuChoice(string menuChoice)
    {
        switch (menuChoice)
        {
            case "Add contacts":
                DatabaseQueries.Run.InsertContact();
                break;
            case "Delete contacts":
                DatabaseQueries.Run.DeleteContact();
                break;
            case "Edit contact":
                DatabaseQueries.Run.UpdateContact();
                break;
            case "View contacts":
                PresentationLayer.ShowTable(DatabaseQueries.Run.GetContactsByCategory(ChooseCategory()));
                break;
            case "Send email":
                MailService.Run.SendMailMessage(ChooseMailMessageDetails());
                break;
            case "Exit":
                return true;
        }
        AnsiConsole.Markup("[green]Operation completed. Press any key to continue.[/]");
        Console.ReadLine();
        return false;
    }

    public static Contact ChooseContactProperties(Contact contact)
    {
        contact.Name = Validator.ValidateName();
        contact.Email = Validator.ValidateEmail();
        contact.PhoneNumber = Validator.ValidatePhoneNumber();
        contact.Category = Validator.ValidateCategory();

        return contact;
    }

    public static bool PerformAgain(string actionType)
    {
        return AnsiConsole.Prompt(new TextPrompt<bool>($"Do you want to {actionType} another contact?")
            .AddChoice(true)
            .AddChoice(false)
            .DefaultValue(false)
            .WithConverter(choice => choice ? "yes" : "no"));
    }

    public static int ChooseContact()
    {
        List<Contact> contactList = DatabaseQueries.Run.GetAllContacts();
        List<string> choicesList = [];
        foreach (var contact in contactList)
        {
            choicesList.Add($"{contact.Id}, {contact.Name}, {contact.Email}, {contact.PhoneNumber}, {contact.Category}");
        }

        string choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose a contact:")
            .AddChoices(choicesList));

        choice = choice.Remove(choice.IndexOf(","));
        return Convert.ToInt32(choice);
    }

    private static string ChooseCategory()
    {
        List<string> categoryList = DatabaseQueries.Run.GetCategories();
        categoryList.Add("All contacts");

        return AnsiConsole.Prompt(new SelectionPrompt<string>()
                        .Title("Choose a category or view all contacts")
                        .AddChoices(categoryList));
    }

    private static MimeMessage ChooseMailMessageDetails()
    {
        using (var context = new PhonebookContext())
        {
            Contact contact = DatabaseQueries.Run.GetSingleContact(context);

            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("test account", "test.mailkit.jakub@gmail.com"));
            mailMessage.To.Add(new MailboxAddress($"{contact.Name}", $"{contact.Email}"));
            mailMessage.Subject = Validator.ValidateString("subject");
            mailMessage.Body = new TextPart("plain")
            {
                Text = Validator.ValidateString("body")
            };
            return mailMessage;
        }
    }
}
