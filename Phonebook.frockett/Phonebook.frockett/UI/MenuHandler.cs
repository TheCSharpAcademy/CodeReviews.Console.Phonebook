using Spectre.Console;
using Phonebook.frockett.Service_Layer;
using Phonebook.frockett.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.frockett.UI;

public class MenuHandler
{
    private readonly PhonebookService phonebookService;
    private readonly InputValidator validator;
    private readonly TableEngine tableEngine;
    private readonly HandleUserInput userInput;

    public MenuHandler(PhonebookService phonebookService, InputValidator inputValidator, TableEngine tableEngine, HandleUserInput userInput)
    {
        this.phonebookService = phonebookService;
        validator = inputValidator;
        this.tableEngine = tableEngine;
        this.userInput = userInput;
    }

    public void ShowMainMenu()
    {
        AnsiConsole.Clear();

        string[] menuOptions =
                {"View Contacts", "Add/Delete Groups", "Exit Program"};

        string choice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Which operation would you like to perform? Use [green]arrow[/] and [green]enter[/] keys to make a selection.")
                            .PageSize(10)
                            .MoreChoicesText("Keep scrolling for more options")
                            .AddChoices(menuOptions));

        int menuSelection = Array.IndexOf(menuOptions, choice) + 1;

        switch (menuSelection)
        {
            case 1:
                HandleContactsMenu();
                break;
            case 2:
                HandleGroupMenu();
                break;
            case 3:
                Environment.Exit(0);
                break;
        }
    }

    private void HandleContactsMenu()
    {
        AnsiConsole.Clear();

        string[] menuOptions =
                {"View All Contacts", "View Group of Contacts", "Add New Contact", "Return to Main Menu"};

        string choice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Which operation would you like to perform? Use [green]arrow[/] and [green]enter[/] keys to make a selection.")
                            .PageSize(10)
                            .MoreChoicesText("Keep scrolling for more options")
                            .AddChoices(menuOptions));

        int menuSelection = Array.IndexOf(menuOptions, choice) + 1;

        switch (menuSelection)
        {
            case 1:
                // TODO print all contacts
                break;
            case 2:
                // TODO print groups, let user select group
                break;
            case 3:
                HandleAddContact();
                break;
            case 4:
                ShowMainMenu();
                break;
        }
    }

    private void HandleContactSubmenu(ContactDTO contact)
    {
        AnsiConsole.Clear();

        string[] menuOptions =
                {"Edit Contact", "Add/Remove Contact from Group", "Delete Contact", "Return to Main Menu"};

        string choice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Which operation would you like to perform? Use [green]arrow[/] and [green]enter[/] keys to make a selection.")
                            .PageSize(10)
                            .MoreChoicesText("Keep scrolling for more options")
                            .AddChoices(menuOptions));

        int menuSelection = Array.IndexOf(menuOptions, choice) + 1;

        switch (menuSelection)
        {
            case 1:
                EditContact(contact);
                break;
            case 2:
                AddRemoveGroup(contact);
                break;
            case 3:
                phonebookService.DeleteContact(contact);
                break;
            case 4:
                ShowMainMenu();
                break;
        }
    }

    private void AddRemoveGroup(ContactDTO oldContact)
    {
        ContactDTO updatedContact = new();
        ContactGroupDTO groupSelection = new();

        if (oldContact.ContactGroupName != null)
        {
            if (AnsiConsole.Confirm("Remove from current group? "))
            {
                updatedContact.ContactGroupName = null;
            }
            else
            {
                return;
            }
        }
        else
        {
            if (AnsiConsole.Confirm("Add contact to group? "))
            {
                //groupSelection = SelectGroup();
            }
            else
            {
                return;
            }
        }

        updatedContact.ContactGroupName = groupSelection.Name;
        updatedContact.Name = oldContact.Name;
        updatedContact.PhoneNumber = oldContact.PhoneNumber;
        updatedContact.Email = oldContact.Email;
    }

    private void EditContact(ContactDTO oldContact)
    {
        ContactDTO updatedContact = new();

        if (AnsiConsole.Confirm("Replace name? ")) 
        {
            updatedContact.Name = GetName();
        }
        else
        {
            updatedContact.Name = oldContact.Name;
        }

        if (AnsiConsole.Confirm("Edit email? "))
        {
            updatedContact.Email = GetEmail();
        }
        else
        {
            updatedContact.Email = oldContact.Email;
        }

        if (AnsiConsole.Confirm("Edit phone number? "))
        {
            updatedContact.PhoneNumber = GetPhoneNumber();
        }
        else
        {
            updatedContact.PhoneNumber = oldContact.PhoneNumber;
        }

        if (oldContact.ContactGroupName != null)
        {
            updatedContact.ContactGroupName = oldContact.ContactGroupName;
        }

        // todo pass oldContact to service method to delete ?? (maybe this isn't a good idea since it isn't a true update)
        // TODO pass updated contact to method to update
    }

    private void HandleAddContact()
    {
        string name = GetName();
        string email = GetEmail();
        string phoneNumber = GetPhoneNumber();

        // TODO pass to service layer method
    }

    private string GetPhoneNumber()
    {
        string newPhoneNumber = AnsiConsole.Ask<string>("Enter the contact's phone number (Format 0123456789 or 012-345-6789): ");
        while (!validator.IsValidPhoneNumber(newPhoneNumber))
        {
            newPhoneNumber = AnsiConsole.Ask<string>($"{newPhoneNumber} is not a valid number. Use format 0123456789 or 012-345-6789. Enter number: ");
        }

        return newPhoneNumber;
    }

    private string GetEmail()
    {
        string newEmail = AnsiConsole.Ask<string>("Enter the contact's email: ");
        while (!validator.IsValidEmail(newEmail))
        {
            newEmail = AnsiConsole.Ask<string>($"{newEmail} is not a valid email. Use the full address, ex. user@domain.com. Enter email: ");
        }

        return newEmail;
    }

    private string GetName()
    {
        string newName = AnsiConsole.Ask<string>("Enter the contact's name: ");
        while (!validator.IsValidName(newName))
        {
            newName = AnsiConsole.Ask<string>($"{newName} is not a valid name. Name can't be null or empty. Enter a valid name: ");
        }

        return newName;
    }

    private void HandleGroupMenu()
    {
        AnsiConsole.Clear();

        string[] menuOptions =
                {"Add group", "Delete Group", "Edit Group", "Exit Program"};

        string choice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Which operation would you like to perform? Use [green]arrow[/] and [green]enter[/] keys to make a selection.")
                            .PageSize(10)
                            .MoreChoicesText("Keep scrolling for more options")
                            .AddChoices(menuOptions));

        int menuSelection = Array.IndexOf(menuOptions, choice) + 1;

        switch (menuSelection)
        {
            case 1:
                //HandleAddGroup();
                break;
            case 2:
                //HandleDeleteGroup();
                break;
            case 3:
                //HandleEditGroup();
                break;
            case 4:
                Environment.Exit(0);
                break;
        }
    }


    private ContactDTO SelectContact(List<ContactDTO> contacts)
    {
        AnsiConsole.Clear();

        var selectOptions = new SelectionPrompt<ContactDTO>();
        selectOptions.AddChoice(new ContactDTO { Email = "0" }); // Use what would otherwise be an invalid email to identify the "cancel" button
        selectOptions.AddChoices(contacts);
        selectOptions.UseConverter(contact => (contact.Email == "0" ? "Cancel" : $"{contact.Name} - {contact.PhoneNumber} - {contact.Email}") // if email is 0 it's a cancel button
                                                + (contact.ContactGroupName != null ? $" - {contact.ContactGroupName}" : "")); // if contact has group name, add it too
        selectOptions.Title("Select the group using the arrow and enter keys");
        selectOptions.MoreChoicesText("Keep scrolling for more");

        ContactDTO selectedContact = AnsiConsole.Prompt(selectOptions);

        return selectedContact;
    }

    private ContactGroupDTO SelectGroup(List<ContactGroupDTO> contactGroups)
    {
        AnsiConsole.Clear();
        var selectOptions = new SelectionPrompt<ContactGroupDTO>();
        selectOptions.AddChoice(new ContactGroupDTO { Name = " " }); // invalid group name is used to identfy the cancel button
        selectOptions.AddChoices(contactGroups);
        selectOptions.UseConverter(group => (group.Name == "0" ? "Cancel" : $"{group.Name}")); // if email is 0 it's a cancel button 
        selectOptions.Title("Select the group using the arrow and enter keys for more options");
        selectOptions.MoreChoicesText("Keep scrolling for more");

        ContactGroupDTO selectedGroup = AnsiConsole.Prompt(selectOptions);

        return selectedGroup;
    }
}
