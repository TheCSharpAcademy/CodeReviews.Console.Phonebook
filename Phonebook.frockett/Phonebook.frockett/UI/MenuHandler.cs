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
                {"Contacts", "Add/Delete Groups", "Exit Program"};

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
                HandleContactSubmenu(userInput.SelectContact(phonebookService.FetchContactList()));
                break;
            case 2:
                ContactGroupDTO selectedGroup = userInput.SelectGroup(phonebookService.FetchGroupList());
                HandleContactSubmenu(userInput.SelectContact(selectedGroup.Contacts));
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
        // check if the email is set to an invalid property, which means it's the sneaky CANCEL button.
        if (contact.Email == "0")
        {
            ShowMainMenu();
            return;
        }

        AnsiConsole.Clear();

        tableEngine.DisplayContact(contact);
        AnsiConsole.WriteLine();

        string[] menuOptions =
                {"Edit Contact", "Add/Remove Contact from Group", "Delete Contact", "Return to Main Menu"};

        string choice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("What would you like to do?")
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

    private void AddRemoveContactFromGroup(ContactDTO contact)
    {
        //ContactDTO updatedContact = new();
        ContactGroupDTO? groupSelection = new();

        if (contact.ContactGroupName != null)
        {
            if (AnsiConsole.Confirm("Remove from current group? "))
            {
                // TODO remove from group in better way
                groupSelection.Name = contact.ContactGroupName;
                phonebookService.RemoveContactFromGroup(groupSelection, contact.Id);
            }
        }
        else
        {
            if (AnsiConsole.Confirm("Add contact to group? "))
            {
                groupSelection = userInput.SelectGroup(phonebookService.FetchGroupList());
                phonebookService.AddContactToGroup(groupSelection, contact.Id);
            }
        }
        ShowMainMenu();
    }

    private void HandleEditContact(ContactDTO oldContact)
    {
        ContactDTO updatedContact = userInput.GetEditedContact(oldContact);

        phonebookService.UpdateContact(updatedContact);

        ShowMainMenu();
    }

    private void HandleAddContact()
    {
        string name = userInput.GetName();
        string email = userInput.GetEmail();
        string phoneNumber = userInput.GetPhoneNumber();

        phonebookService.AddContact(name, email, phoneNumber);

        ShowMainMenu();
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
        ContactGroupDTO groupToEdit = userInput.SelectGroup(phonebookService.FetchGroupList());
        string newName = userInput.GetName();

        while (phonebookService.CheckForGroupName(newName))
        {
            AnsiConsole.MarkupLine("[red]Sorry, this group name is already taken.[red] Each group needs a unique name.");
            newName = userInput.GetName();
        }

        phonebookService.UpdateGroupName(newName);
        ShowMainMenu();
    }

    private void HandleDeleteGroup()
    {
        ContactGroupDTO groupToDelete = userInput.SelectGroup(phonebookService.FetchGroupList());
        bool deleteRelatedContacts;

        deleteRelatedContacts = AnsiConsole.Confirm($"Delete all contacts in {groupToDelete.Name}? ");

        phonebookService.DeleteGroup(groupToDelete, deleteRelatedContacts);
        ShowMainMenu();
    }

    private void HandleAddGroup()
    {
        string groupName = userInput.GetName();

        while (phonebookService.CheckForGroupName(groupName))
        {
            AnsiConsole.MarkupLine("[red]Sorry, this group name is already taken.[red] Each group needs a unique name.");
            groupName = userInput.GetName();
        }
        phonebookService.AddNewGroup(groupName);
        ShowMainMenu();
    }
}
