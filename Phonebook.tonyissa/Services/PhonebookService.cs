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
        using var context = new PhonebookContext();
        var name = UserInputHandler.GetName();
        var contacts = await PhonebookRepository.GetEntryFromName(context, name);
        MenuController.PrintContacts(contacts);

        var selectedIndex = UserInputHandler.GetContactPosFromList(contacts);
        MenuController.PrintContacts([contacts[selectedIndex]]);

        AnsiConsole.Write("\nPress any key to continue...");
        Console.ReadKey();
    }
}