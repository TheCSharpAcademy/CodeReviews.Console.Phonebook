using PhoneBook.Data.Models;
using PhoneBook.Helpers;
using PhoneBook.Services;
using Spectre.Console;

namespace PhoneBook
{
  public static class PhoneBookController
  {
    private static readonly PhonebookDbContext _context = new PhonebookDbContext();
    private static readonly ContactService _contactService = new ContactService(_context);

    public static void AddContact()
    {
      try
      {
        var contact = _contactService.GetValidatedContact();
        if (contact == null) return; // Exit if user chose not to continue

        _contactService.AddContact(contact);
        AnsiConsole.MarkupLine("[green]\nNew Contact has been added successfully![/]");
        Helper.ContinueMessage();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    public static void UpdateContact()
    {
      try
      {
        var contactEmail = AnsiConsole.Ask<string>("Enter the contact's email you want to update: ");

        var contact = _contactService.GetContact(contactEmail);

        UserInterface.ShowContact(contact);
        var updatedContact = _contactService.GetValidatedContact();
        if (updatedContact == null) return; // Exit if user chose not to continue

        _contactService.UpdateContact(contactEmail, updatedContact);
        AnsiConsole.MarkupLine("[green]Contact updated successfully![/]");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    public static void DeleteContact()
    {
      var result = _contactService.GetAllContacts();

      UserInterface.ShowContacts(result);

      var contactId = AnsiConsole.Ask<string>("Enter the contact's email you want to delete: ");

      try
      {
        _contactService.DeleteContact(contactId);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
    public static void ViewContact()
    {
      var contactEmail = AnsiConsole.Ask<string>("Enter the contact's email you want to get: ");

      try
      {
        var contact = _contactService.GetContact(contactEmail);

        if (contact != null)
        {
          UserInterface.ShowContact(contact);
          Helper.ContinueMessage();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
    public static void ViewAllContacts()
    {
      var result = _contactService.GetAllContacts();

      UserInterface.ShowContacts(result);

      Helper.ContinueMessage();
    }
  }
}
