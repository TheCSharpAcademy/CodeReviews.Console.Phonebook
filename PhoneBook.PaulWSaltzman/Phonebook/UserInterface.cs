using Phonebook.Controllers;
using Phonebook.Models;
using Phonebook.Services;
using Spectre.Console;
using static Phonebook.Models.Enums;

namespace Phonebook;


static internal class UserInterface
{
    internal static void MainMenu()
    {
        bool exitProgram = false;
        while (!exitProgram)
        {

            Console.Clear();
            AnsiConsole.Write(
                renderable: new FigletText("Console Phonebook")
                .LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                .Title("What would you like to do?")
                .AddChoices(
                    MainMenuOptions.Messaging,
                    MainMenuOptions.Sent,
                    MainMenuOptions.ManageContacts,
                    MainMenuOptions.Settings,
                    MainMenuOptions.Quit));

            switch (option)
            {
                case MainMenuOptions.Messaging:
                    var contact = ContactService.GetContactOptionInput();
                    string title = $@"How would you like to communicate with {contact.FirstName} {contact.LastName}";
                    MessagingOptions messagingOption = MessagingOptionsMenu(title);
                    MessageService.RouteMessage(contact, messagingOption);
                    break;
                case MainMenuOptions.Sent:
                    title = "What history would you like to see";
                    messagingOption = MessagingOptionsMenu(title);
                    MessageService.ViewHistory(messagingOption);
                    break;
                case MainMenuOptions.ManageContacts:
                    ManageContactsMenu();
                    break;
                case MainMenuOptions.Settings:
                    ViewSettings();
                    break;
                case MainMenuOptions.Quit:
                    exitProgram = true;
                    break;

            }
        }
    }


    internal static MessagingOptions MessagingOptionsMenu(string title)
    {
        bool exitProgram = false;
        while (!exitProgram)
        {

            Console.Clear();
            AnsiConsole.Write(
                renderable: new FigletText("Console Phonebook")
                .LeftJustified());
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MessagingOptions>()
                .Title(title)
                .AddChoices(
                    MessagingOptions.Email,
                    MessagingOptions.SMS));

            switch (option)
            {
                case MessagingOptions.Email:
                    return MessagingOptions.Email;
                    break;
                case MessagingOptions.SMS:
                    return MessagingOptions.SMS;
                    break;
                case MessagingOptions.Back:
                    exitProgram = true;
                    break;

            }
        }
        return MessagingOptions.Back;
    }

    internal static void ManageContactsMenu()
    {

        bool exitMenu = false;
        while (!exitMenu)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                    new SelectionPrompt<ManageContactsMenuOptions>()
                    .Title("Manage Contacts")
                    .AddChoices(
                        ManageContactsMenuOptions.AddContact,
                        ManageContactsMenuOptions.ManageContactMethods,
                        ManageContactsMenuOptions.UpdateContact,
                        ManageContactsMenuOptions.DeleteContact,
                        ManageContactsMenuOptions.Back));

            switch (option)
            {
                case ManageContactsMenuOptions.AddContact:
                    ContactService.AddContact();
                    break;
                case ManageContactsMenuOptions.ManageContactMethods:
                    var contact = ContactService.GetContactOptionInput();
                    ManageContactMethods(contact);
                    break;
                case ManageContactsMenuOptions.UpdateContact:
                    contact = ContactService.GetContactOptionInput();
                    contact = ContactService.UpdateContact(contact);
                    ShowSingleContact(contact);
                    Console.WriteLine("Press any key to Continue.");
                    Console.ReadKey();
                    break;
                case ManageContactsMenuOptions.DeleteContact:
                    contact = ContactService.GetContactOptionInput();
                    ContactController.DeleteContact(contact);
                    break;
                case ManageContactsMenuOptions.Back:
                    exitMenu = true;
                    break;
            }
        }
    }

    private static void ViewSettings()
    {
        bool inMenu = true;
        while (inMenu)
        {
            Console.Clear();
            Settings settings = SettingsController.GetSettings();

            SettingsService.ShowSettings(settings);



            var option = AnsiConsole.Prompt(
                new SelectionPrompt<SettingsMenuOptions>()
                .AddChoices(
                    SettingsMenuOptions.ModifySettings,
                    SettingsMenuOptions.RemoveAllSettings,
                    SettingsMenuOptions.Back));

            switch (option)
            {
                case SettingsMenuOptions.ModifySettings:
                    bool settingsExist = SettingsService.CheckSettings();
                    if (settingsExist)
                    {
                        settings = SettingsService.UpdateSettings(settings);
                    }
                    else
                    {
                        SettingsService.GetSettingsFromUserFirstTime();
                    }
                    break;
                case SettingsMenuOptions.RemoveAllSettings:
                    SettingsService.RemoveSettings(settings);
                    Console.ReadKey();
                    inMenu = false;
                    break;
                case SettingsMenuOptions.Back:
                    inMenu = false;
                    break;
            }
        }
    }

    internal static void ManageContactMethods(Contact contact)
    {
        bool inMenu = true;
        while (inMenu)
        {
           
            Console.Clear();
            ShowSingleContact(contact);

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<ManageContactMethodsMenu>()
                .AddChoices(
                    ManageContactMethodsMenu.ManageEmails,
                    ManageContactMethodsMenu.ManagePhoneNumbers,
                    ManageContactMethodsMenu.Back));

            switch (option)
            {
                case ManageContactMethodsMenu.ManageEmails:
                    ManageEmailMenu(contact);
                    break;
                case ManageContactMethodsMenu.ManagePhoneNumbers:
                    ManagePhoneNumberMenu(contact);
                    break;
                case ManageContactMethodsMenu.Back:
                    inMenu = false;
                    break;
            }
        }
    }

    private static void ManagePhoneNumberMenu(Contact contact)
    {
        bool inMenu = true;
        while (inMenu)
        {
            Console.Clear();
            ShowSingleContact(contact);

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<PhoneMenuOptions>()
                .AddChoices(
                    PhoneMenuOptions.AddPhoneNumber,
                    PhoneMenuOptions.RemovePhoneNumber,
                    PhoneMenuOptions.Back));
            switch (option)
            {
                case PhoneMenuOptions.AddPhoneNumber:
                    PhoneService.AddPhone(contact);
                    break;
                case PhoneMenuOptions.RemovePhoneNumber:
                    if (contact.PhoneNumbers.Count <= 0)
                    {
                        Console.WriteLine("There are no phone numbers for this contact. Press any key to continue.");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        Phone phoneToDelete = PhoneService.GetPhoneOptionInput(contact);
                        PhoneService.DeletePhone(phoneToDelete);
                        contact = PhoneService.RemovePhoneFromContact(phoneToDelete, contact);
                    }
                    break;
                case PhoneMenuOptions.Back:
                    inMenu = false;
                    break;
            }
        }
    }

    private static void ManageEmailMenu(Contact contact)
    {

        bool inMenu = true;
        while (inMenu)
        {
            Console.Clear();
            ShowSingleContact(contact);

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<EmailMenuOptions>()
                .AddChoices(
                    EmailMenuOptions.AddEmail,
                    EmailMenuOptions.RemoveEmail,
                    EmailMenuOptions.Back));
            switch (option)
            {
                case EmailMenuOptions.AddEmail:
                    EmailServices.AddEmail(contact);
                    break;
                case EmailMenuOptions.RemoveEmail:
                    if (contact.Emails.Count <= 0)
                    {
                        Console.WriteLine("There are no emails for this contact. Press any key to continue.");
                        return;
                    }
                    else
                    {
                        Email emailToDelete = EmailServices.GetEmailOptionInput(contact);
                        EmailServices.DeleteEmail(emailToDelete);
                        contact = EmailServices.RemoveEmailFromContact(emailToDelete, contact);
                        break;
                    }
                case EmailMenuOptions.Back:
                    inMenu = false;
                    break;
            }
        }
    }

    internal static void ShowSingleContact(Contact contact)
    {
        string contactInfo = string.Join("\n", $"ID: {contact.ContactId}", $"Lastname: {contact.LastName}");

        string emailsString;
        if (contact.Emails != null && contact.Emails.Any())
        {
            // Concatenate emails into a single string
            emailsString = string.Join("\n", contact.Emails.Select(e => e.EmailAddress));
        }
        else
        {
            emailsString = "No Emails";
        }

        string phoneNumberString;
        if (contact.PhoneNumbers != null && contact.PhoneNumbers.Any())
        {
            // Concatenate phone numbers into a single string
            phoneNumberString = string.Join("\n", contact.PhoneNumbers.Select(p => p.PhoneNumber));
        }
        else
        {
            phoneNumberString = "No Phone Numbers";
        }

        var table = new Table();

        table.AddColumn("Contact");
        table.AddColumn("Email");
        table.AddColumn("Phone Number");

        table.AddRow(contactInfo, emailsString, phoneNumberString);
        AnsiConsole.Write(table);
    }




    internal static void ShowContacts(List<Contact> contacts)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Nickname");
        table.AddColumn("FirstName");
        table.AddColumn("LastName");

        foreach (var contact in contacts)
        {
            table.AddRow(
                contact.ContactId.ToString(),
                contact.FirstName,
                contact.LastName);
        }

        AnsiConsole.Write(table);
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }

}
