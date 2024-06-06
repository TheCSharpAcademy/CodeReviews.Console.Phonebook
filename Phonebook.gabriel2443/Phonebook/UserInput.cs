using Phonebook.Data;
using Phonebook.Models;
using Spectre.Console;

namespace Phonebook;

internal class UserInput
{
    private ContactDetailsService contactService = new ContactDetailsService();

    public void Menu()
    {
        Console.Clear();
        bool isRunning = true;
        while (isRunning)
        {
            var select = new SelectionPrompt<string>();
            select.Title("\n[bold]What would you like to do?[/]\n");
            select.AddChoice("Add a contact");
            select.AddChoice("View all contacts");
            select.AddChoice("Edit a contact");
            select.AddChoice("Delete a contact");
            select.AddChoice("Exit the application");
            var selectedChoice = AnsiConsole.Prompt(select);

            switch (selectedChoice)
            {
                case "Add a contact":
                    AddContactDetail();
                    break;

                case "View all contacts":
                    GetContactDetails();
                    break;

                case "Edit a contact":
                    UpdateDetails();
                    break;

                case "Delete a contact":
                    DeleteContactDetails();
                    break;

                case "Exit the application":
                    isRunning = false;
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private void AddContactDetail()
    {
        string name = AnsiConsole.Ask<string>($"Please enter the [green]name[/] you want to add?");
        AnsiConsole.Markup($"Please enter the [green]email[/] you want to add?");
        var email = Console.ReadLine();
        while (!Helpers.IsValidEmail(email))
        {
            AnsiConsole.Markup("[red]Invalid email format, please enter another email[/]");
            email = Console.ReadLine();
        }

        AnsiConsole.Markup("$\"Please enter the [green]phone number in the [bold]U.S[/] format[/]");
        string phoneNum = Console.ReadLine();

        while (!Helpers.IsValidPhoneNum(phoneNum))
        {
            AnsiConsole.Markup("[red]Invalid phone number format, please enter another phone number[/]");
            phoneNum = Console.ReadLine();
        }

        contactService.AddContact(name, email, phoneNum);
    }

    private void GetContactDetails()
    {
        Console.Clear();
        var details = contactService.GetContacts();

        var grid = new Grid();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddRow(new string[] { "No.", "Name", "Email", "Phone Number" });
        int counter = 0;
        foreach (var c in details)
        {
            grid.AddRow(new string[] { $"{++counter}", $"{c.Name}", $"{c.Email}", $"{c.Phone}" });
        }
        AnsiConsole.Write(grid);
        Console.WriteLine("Press any key to go back to menu");
        Console.ReadLine();
    }

    private void UpdateDetails()
    {
        Console.Clear();
        var contactDetails = contactService.GetContacts();

        var select = new SelectionPrompt<ContactDetails>();
        select.AddChoices(contactDetails);
        select.AddChoice(new ContactDetails { Name = "Go back to menu" });
        select.UseConverter(contact => $"{contact.Name} {contact.Email} {contact.Phone}");
        var selectedChoice = AnsiConsole.Prompt(select);
        if (selectedChoice.Name == "Go back to menu") return;
        string name = AnsiConsole.Ask<string>($"Please enter the [green]name[/] you want to add?");
        AnsiConsole.Markup($"Please enter the [green]email[/] you want to add?");
        var email = Console.ReadLine();
        while (!Helpers.IsValidEmail(email))
        {
            AnsiConsole.Markup("[red]Invalid email format, please enter another email[/]");
            email = Console.ReadLine();
        }

        AnsiConsole.Markup("Please enter the [green]phone number in the [bold]U.S[/] format[/]");
        string phoneNum = Console.ReadLine();

        while (!Helpers.IsValidPhoneNum(phoneNum))
        {
            AnsiConsole.Markup("[red]Invalid phone number format, please enter a valid phone number [/]");
            phoneNum = Console.ReadLine();
        }
        selectedChoice.Name = name;
        selectedChoice.Email = email;
        selectedChoice.Phone = phoneNum;

        contactService.EditContacts(selectedChoice);
    }

    private void DeleteContactDetails()
    {
        Console.Clear();
        var contactDetails = contactService.GetContacts();
        var select = new SelectionPrompt<ContactDetails>();
        select.Title("[bold]Select a contact you want to delete[/]");
        select.AddChoice(new ContactDetails { Id = 0, Name = "Go back to menu" });
        select.AddChoices(contactDetails);
        select.UseConverter(contact => $"{contact.Name} {contact.Email} {contact.Phone}");
        var selectedChoice = AnsiConsole.Prompt(select);
        if (selectedChoice.Id == 0) return;

        if (AnsiConsole.Confirm($"Are you sure you want to delete the selected option({selectedChoice.Name.ToUpper()}) and all information?"))
        {
            AnsiConsole.WriteLine($"You deleted the {selectedChoice.Name.ToUpper()} contact and all its information");
        }
        contactService.DeleteContacts(selectedChoice);
    }
}