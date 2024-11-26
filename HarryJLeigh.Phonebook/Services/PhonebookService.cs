using Phonebook.Data;
using Phonebook.Models;
using Phonebook.Utilities;
using Phonebook.Views;
using Spectre.Console;

namespace Phonebook.Services;

public static class PhonebookService
{
    internal static void ViewContacts()
    {
        Console.Clear();
        var contacts = GetAllContacts();
        if (contacts.Count == 0)
            AnsiConsole.MarkupLine("[yellow]No contacts found![/]");
        else
            TableVisualisation.ShowTable(contacts);
    }

    internal static void ViewContactsByFilter(string filter)
    {
        using var context = new AppDbContext();
        List<Contact> filteredContacts = context.Contacts.Where(c => c.Category == filter).ToList();
        TableVisualisation.ShowTable(filteredContacts);
        Util.AskUserToContinue();
    }

    internal static void CreateContact(Contact contact)
    {
        if (Util.ReturnToMenu()) return;
        using var context = new AppDbContext();
        context.Contacts.Add(contact);
        context.SaveChanges();
        AnsiConsole.MarkupLine("[yellow]Success! Created new contact.[/]");
        Util.AskUserToContinue();
    }

    internal static void UpdateContact(bool updateName = false, bool updateEmail = false, bool updateNumber = false, bool updateCategory = false)
    {
        ViewContacts();

        List<int> contactIds = GetContactsId();
        if (contactIds.Count == 0)
        {
            Util.AskUserToContinue();
            return;
        }

        int contactId = UserInputHelper.GetId(contactIds, "update");

        using var context = new AppDbContext();
        var contact = context.Contacts.Find(contactId);

        if (updateName == true) contact.Name = UserInputHelper.GetName("update");
        if (updateEmail == true) contact.Email = UserInputHelper.GetEmail("update");
        if (updateNumber == true) contact.PhoneNumber = UserInputHelper.GetPhoneNumber("update");
        if (updateCategory == true) contact.Category = UserInputHelper.GetCategory("update");
        if (Util.ReturnToMenu()) return;

        context.SaveChanges();
        AnsiConsole.MarkupLine("[yellow]Success! Updated contact.[/]");
        Util.AskUserToContinue();
    }

    internal static void DeleteContact()
    {
        ViewContacts();

        List<Contact> contacts = GetAllContacts();
        if (contacts.Count == 0)
        {
            Util.AskUserToContinue();
            return;
        }

        using var context = new AppDbContext();
        List<int> contactIds = GetContactsId();
        int contactId = UserInputHelper.GetId(contactIds, "delete");

        if (Util.ReturnToMenu()) return;
        var contact = context.Contacts.Find(contactId);
        context.Contacts.Remove(contact);
        context.SaveChanges();
    }

    internal static List<int> GetContactsId()
    {
        using var context = new AppDbContext();
        return context.Contacts.Select(c => c.Id).ToList();
    }

    private static List<Contact> GetAllContacts()
    {
        using var context = new AppDbContext();
        return context.Contacts.ToList();
    }

    internal static List<Contact> GetContactById(int id)
    {
        using var context = new AppDbContext();
        return context.Contacts.Where(c => c.Id == id).ToList();
    }
}