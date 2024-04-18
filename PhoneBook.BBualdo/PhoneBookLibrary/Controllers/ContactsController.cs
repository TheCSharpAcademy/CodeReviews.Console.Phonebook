using Microsoft.EntityFrameworkCore;
using PhoneBookLibrary.Models;
using Spectre.Console;

namespace PhoneBookLibrary.Controllers;

public static class ContactsController
{
  public static void DeleteContact(Contact contact)
  {
    using PhoneBookContext db = new();
    db.Remove(contact);
    int stateChanges = db.SaveChanges();

    if (stateChanges == 0) AnsiConsole.Markup("[red]Contact deleting failed. [/]");
    else AnsiConsole.Markup("[green]Contact deleted! [/]");
  }

  public static List<Contact>? GetAllContacts()
  {
    using PhoneBookContext db = new();

    if (db.Contacts.Include(contact => contact.Group).ToList().Count == 0)
    {
      AnsiConsole.Markup("[red]Contacts list is empty.[/] Create one first. ");
      return null;
    }

    return [.. db.Contacts];
  }

  public static Contact? GetContactById(int contactId)
  {
    using PhoneBookContext db = new();
    Contact? contact = db.Contacts.Include(contact => contact.Group).FirstOrDefault(contact => contact.ContactId == contactId);

    if (contact == null)
    {
      AnsiConsole.Markup("[red]Contact with given ID doesn't exists. [/]\n");
      return null;
    }

    return contact;
  }

  public static void InsertContact(Contact contact)
  {
    using PhoneBookContext db = new();

    db.Add(contact);
    int stateChanges = db.SaveChanges();

    if (stateChanges == 0) AnsiConsole.Markup("[red]Contact adding failed. [/]");
    else AnsiConsole.Markup("[green]Contact added! [/]");
  }

  public static void UpdateContact(Contact contact)
  {
    using PhoneBookContext db = new();

    db.Update(contact);
    int stateChanges = db.SaveChanges();

    if (stateChanges == 0) AnsiConsole.Markup("[red]Contact updating failed. [/]");
    else AnsiConsole.Markup("[green]Contact updated! [/]");
  }
}