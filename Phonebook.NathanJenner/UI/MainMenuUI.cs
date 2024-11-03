using Spectre.Console;

namespace Console.Phonebook.App.UI;

public class MainMenuUI
{
    CreateContactUI createNewContact = new CreateContactUI();
    ViewAllContactsUI viewAllContacts = new ViewAllContactsUI();
    DeleteContactUI deleteContact = new DeleteContactUI();
    public void MainMenuSelection()
    {
        var mainMenuSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Please select from the options below: [/]")
                .PageSize(20)
                .AddChoices(new[] {
                    "View all Contacts", "Add new Contact", "Update a Contact", "Delete a Contact", "Exit application"
                }));

        switch (mainMenuSelection)
        {
            case "View all Contacts": viewAllContacts.ViewContacts(); break;
            case "Add new Contact": createNewContact.CreateNewContact(); break;
            case "Update a Contact": createNewContact.CreateNewContact(); break;
            case "Delete a Contact": deleteContact.DeleteContact(); break;
            case "Exit application": Environment.Exit(0); break;
        }
    }
}