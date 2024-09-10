using Phonebook.tonyissa.Context;
using Phonebook.tonyissa.Repositories;
using Phonebook.tonyissa.UI;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.tonyissa.Services;
public static class PhonebookService
{
    public static async Task GetAllContactsAsync()
    {
        using var context = new PhonebookContext();
        var list = await PhonebookRepository.GetAllEntriesAsync(context);
        MenuController.PrintContacts(list);

        AnsiConsole.Write("\nPress any key to continue...");
        Console.ReadKey();
    }

    public static async Task GetSingularContact()
    {
        var name = UserInputHandler.GetName("Which name do you want to lookup?");

        using var context = new PhonebookContext();
        var contacts = await PhonebookRepository.GetEntryFromName(context, name);
        MenuController.PrintContacts(contacts);

        if (contacts.Count < 1)
        {
            AnsiConsole.Write("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }

        var selectedIndex = UserInputHandler.GetContactPosFromList(contacts);
        MenuController.PrintContacts([contacts[selectedIndex]]);

        await MenuController.InitContactMenu(contacts[selectedIndex]);
    }

    public static async Task CreateContact()
    {
        var name = UserInputHandler.GetName("Enter a name for your new contact, or type quit to exit:");
        if (name.ToLower() == "quit") return;

        var email = UserInputHandler.GetEmail();
        if (email.ToLower() == "quit") return;

        var phoneNumber = UserInputHandler.GetNumber();
        if (phoneNumber.ToLower() == "quit") return;

        using var context = new PhonebookContext();
        var newContact = new Contact { ID = 0, Name = name, Email = email, PhoneNumber = phoneNumber };
        await PhonebookRepository.AddEntryAsync(context, newContact);

        MenuController.PrintContacts([newContact]);

        AnsiConsole.Write("\nRecord created successfully. Press any key to continue...");
        Console.ReadKey();
    }

    public static async Task UpdateContact(Contact contact)
    {
        var name = UserInputHandler.GetName("Enter a new name for your contact, or type quit to exit:");
        if (name.ToLower() == "quit") return;

        var email = UserInputHandler.GetEmail();
        if (email.ToLower() == "quit") return;

        var phoneNumber = UserInputHandler.GetNumber();
        if (phoneNumber.ToLower() == "quit") return;

        contact.Name = name;
        contact.Email = email;
        contact.PhoneNumber = phoneNumber;

        using var context = new PhonebookContext();
        await PhonebookRepository.UpdateEntryAsync(context, contact);

        AnsiConsole.Write("Record updated successfully. Press any key to continue...");
        Console.ReadKey();
    }

    public static async Task DeleteContact(Contact contact)
    {
        var confirmation = AnsiConsole.Confirm("Are you sure you want to delete this record?");

        if (!confirmation) return;

        using var context = new PhonebookContext();
        await PhonebookRepository.DeleteEntryAsync(context, contact);

        AnsiConsole.Write("Record deleted successfully. Press any key to continue...");
        Console.ReadKey();
    }
}