using Spectre.Console;
using Phonebook.frockett.Service_Layer;
using Phonebook.frockett.DTOs;

namespace Phonebook.frockett.UI;

public class MenuHandler
{
    private readonly PhonebookService phonebookService;
    private readonly TableEngine tableEngine;
    private readonly HandleUserInput userInput;

    public MenuHandler(PhonebookService phonebookService, TableEngine tableEngine, HandleUserInput userInput)
    {
        this.phonebookService = phonebookService;
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
                            .Title("Welcome to your phonebook! Use [green]arrow[/] and [green]enter[/] keys to make a selection.")
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
                            .Title("What would you like to do?")
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
        if (contact == null)
        {
            AnsiConsole.MarkupLine("[green]Press enter to return to contacts menu...[/]");
            Console.ReadLine();
            HandleContactsMenu();
            return;
        }

        // check if the email is set to an invalid property, which means it's the sneaky CANCEL button.
        if (contact.Name != null && contact.Name == " ")
        {
            HandleContactsMenu();
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
                HandleAddOrRemoveFromGroup(contact);
                break;
            case 3:
                HandleDeleteContact(contact);
                break;
            case 4:
                ShowMainMenu();
                break;
        }
    }

    private void HandleAddOrRemoveFromGroup(ContactDTO contact)
    {
        if (contact.ContactGroupName == null)
            AddContactToGroup(contact);
        else
            RemoveContactFromGroup(contact);
    }

    private void HandleDeleteContact(ContactDTO contactToDelete)
    {
        if(phonebookService.DeleteContact(contactToDelete))
            AnsiConsole.MarkupLine("[green]Contact deleted![/]");
        else
            AnsiConsole.MarkupLine("[red]An error occurred while deleting.[/]");
        

        PauseForStatusMessage();
        ShowMainMenu();
    }

    private void RemoveContactFromGroup(ContactDTO contact)
    {
        ContactGroupDTO? group = new();

        if (contact.ContactGroupName == null) // these checks are redundant
        {
            AnsiConsole.WriteLine($"{contact.Name} isn't in any groups.");
        }
        else
        {
            if (AnsiConsole.Confirm($"Are you sure you wish to remove {contact.Name} from the group {contact.ContactGroupName}?"))
            {
                group.Name = contact.ContactGroupName;
                bool isDeleted = phonebookService.RemoveContactFromGroup(group, contact.Id);

                if (isDeleted) 
                    AnsiConsole.MarkupLine($"[green]{contact.Name} removed from group: {group.Name}[/]");
                else 
                    AnsiConsole.MarkupLine("[red]Removal cancelled[/]");
            }
        }

        PauseForStatusMessage();
        ShowMainMenu();
    }

    private void AddContactToGroup(ContactDTO contact)
    {
        if (contact.ContactGroupName != null)
        {
            AnsiConsole.WriteLine($"{contact.Name} is already in group {contact.ContactGroupName}. Contacts can only be in one group at a time.");
        }
        else
        {
            ContactGroupDTO group = userInput.SelectGroup(phonebookService.FetchGroupList());
            bool isAdded = phonebookService.AddContactToGroup(group, contact.Id);

            if (isAdded)
                AnsiConsole.MarkupLine($"[green]{contact.Name} added to group: {group.Name}[/]");
            else
                AnsiConsole.MarkupLine("[red]Addition cancelled[/]");
        }

        PauseForStatusMessage();
        ShowMainMenu();
    }

    private void HandleEditContact(ContactDTO oldContact)
    {
        ContactDTO updatedContact = userInput.GetEditedContact(oldContact);

        if (phonebookService.UpdateContact(updatedContact))
            AnsiConsole.MarkupLine($"[green]{oldContact.Name} has been updated[/]");
        else
            AnsiConsole.MarkupLine("[red]Failure to edit contact, try again.[/]");

        PauseForStatusMessage();
        ShowMainMenu();
    }

    private void HandleAddContact()
    {
        string name = userInput.GetName();
        string email = userInput.GetEmail();
        string phoneNumber = userInput.GetPhoneNumber();

        // Check if a success signal was returned from repository
        if (phonebookService.AddContact(name, email, phoneNumber))
            AnsiConsole.MarkupLine($"[green]{name} was added to contacts.[/]");
        else
            AnsiConsole.MarkupLine("[red]An error occurred, please try again[/]");

        PauseForStatusMessage();
        ShowMainMenu();
    }

    private void HandleGroupMenu()
    {
        AnsiConsole.Clear();

        string[] menuOptions =
                {"Add group", "Delete Group", "Edit Group", "Return to Main Menu"};

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
                HandleEditGroup(userInput.SelectGroup(phonebookService.FetchGroupList()));
                break;
            case 4:
                ShowMainMenu();
                break;
        }
    }

    private void HandleEditGroup(ContactGroupDTO groupToEdit)
    {
        // Check for exit selection
        if (groupToEdit.Name != null && groupToEdit.Name == " ")
        {
            HandleGroupMenu();
            return;
        }

        string newName = userInput.GetName();

        while (phonebookService.CheckForGroupName(newName))
        {
            AnsiConsole.MarkupLine("[red]Sorry, this group name is already taken.[red] Each group needs a unique name.");
            newName = userInput.GetName();
        }

        // Check if a success signal was returned from repository
        if (phonebookService.UpdateGroupName(newName, groupToEdit))
            AnsiConsole.MarkupLine($"[green]Group {groupToEdit.Name} changed to {newName}![/]");
        else
            AnsiConsole.MarkupLine($"[red]Operation failed. Try again.[/]");

        PauseForStatusMessage();
        HandleGroupMenu();
    }

    private void HandleDeleteGroup()
    {
        ContactGroupDTO groupToDelete = userInput.SelectGroup(phonebookService.FetchGroupList());

        // Check for exit code
        if (groupToDelete.Name != null && groupToDelete.Name == " ")
        {
            HandleGroupMenu();
            return;
        }

        bool deleteRelatedContacts;
        deleteRelatedContacts = AnsiConsole.Confirm($"Also delete any contacts in {groupToDelete.Name}? ");

        // Check if a success signal was returned from repository
        if (phonebookService.DeleteGroup(groupToDelete, deleteRelatedContacts))
            AnsiConsole.MarkupLine($"[green]Group {groupToDelete.Name} deleted successfully![/]");
        else
            AnsiConsole.MarkupLine($"[red]Operation failed. Try again.[/]");

        PauseForStatusMessage();
        HandleGroupMenu();
    }

    private void HandleAddGroup()
    {
        string groupName = userInput.GetName();

        while (phonebookService.CheckForGroupName(groupName))
        {
            AnsiConsole.MarkupLine("[red]Sorry, this group name is already taken.[red] Each group needs a unique name.");
            groupName = userInput.GetName();
        }

        // Check if a success signal was returned from repository
        if (phonebookService.AddNewGroup(groupName))
            AnsiConsole.MarkupLine($"[green]Group {groupName} added successfully![/]");
        else
            AnsiConsole.MarkupLine($"[red]Operation failed. Try again.[/]");

        PauseForStatusMessage();
        HandleGroupMenu();
    }
    private static void PauseForStatusMessage()
    {
        AnsiConsole.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }
}
