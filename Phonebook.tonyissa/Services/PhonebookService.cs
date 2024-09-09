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

        var selectedIndex = UserInputHandler.GetContactPosFromList(contacts);
        MenuController.PrintContacts([contacts[selectedIndex]]);

        AnsiConsole.Write("\nPress any key to continue...");
        Console.ReadKey();
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
}