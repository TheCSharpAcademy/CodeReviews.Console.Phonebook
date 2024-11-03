using Console.Phonebook.App.Data;
using Console.Phonebook.App.Entities;
using Spectre.Console;

namespace Console.Phonebook.App.UI;

public class DeleteContactUI
{
    DataServices DbServices = new();

    public void DeleteContact()
    {
        List<Contact> contactsList = DbServices.GetAllContacts();
        MainMenuUI MainMenuUI = new();

        var deleteContactSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Please select from the options below: [/]")
                .PageSize(20)
                .AddChoices(contactsList.Select(l => l.Name))); //lambda - which is essentially foreach l, l.Name

        Contact selectedContact = contactsList.Find(x => x.Name == deleteContactSelection);

        DbServices.DeleteSelectedContact(selectedContact);

        System.Console.Clear();
        System.Console.WriteLine("\n\nContact successfuly deleted.\n\n\n\n\n");
        MainMenuUI.MainMenuSelection();
    }
}