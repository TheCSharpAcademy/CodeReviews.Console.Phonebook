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
                HandleEditContact(contact);
                break;
            case 2:
                AddRemoveContactFromGroup(contact);
                break;
            case 3:
                phonebookService.DeleteContact(contact);
                break;
            case 4:
                ShowMainMenu();
                break;
        }
    }

    private void AddRemoveContactFromGroup(ContactDTO oldContact)
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
                // TODO call service layer function that gets list of groups to display in group selection menu
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

        // TODO call service layer update contact 
    }

    private void HandleEditContact(ContactDTO oldContact)
    {
        ContactDTO updatedContact = userInput.GetEditedContact(oldContact);

        // TODO pass oldContact to service method to delete ?? (maybe this isn't a good idea since it isn't a true update)
        // TODO pass updated contact to method to update
    }

    private void HandleAddContact()
    {
        string name = userInput.GetName();
        string email = userInput.GetEmail();
        string phoneNumber = userInput.GetPhoneNumber();

        // TODO pass to service layer method
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
                HandleAddGroup();
                break;
            case 2:
                HandleDeleteGroup();
                break;
            case 3:
                HandleEditGroup();
                break;
            case 4:
                ShowMainMenu();
                break;
        }
    }

    private void HandleEditGroup()
    {
        //ContactGroupDTO groupToEdit = userInput.SelectGroup();
        string newName = userInput.GetName();

        // TODO Make sure the name isn't already in use by a group

        throw new NotImplementedException();
    }

    private void HandleDeleteGroup()
    {
        // TODO select group menu then send group to be deleted to service layer
        throw new NotImplementedException();
    }

    private void HandleAddGroup()
    {
        string groupName = userInput.GetName();

        // TODO make sure name isn't already in use
        // TODO pass to service layer method to create group
    }
}
