using PhoneBook.Data.Models;
using PhoneBook.Dtos;
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
      // Asking for contact details using Spectre.Console
      var name = AnsiConsole.Ask<string>("Enter the contact's name: ");
      var email = AnsiConsole.Ask<string>("Enter the contact's email: ");
      var phoneNumber = AnsiConsole.Ask<string>("Phone number (format: xxx-xxx-xxxx): ");

      var contact = new ContactDto { Name = name, Email = email, PhoneNumber = phoneNumber };

      _contactService.AddContact(contact);

      Console.WriteLine("\nNew Contact has been added\n");

      Helper.ContinueMessage();
    }

    public static void UpdateContact()
    {
      // Ask for id 
      var contactId = AnsiConsole.Ask<int>("Enter the contact's Id you want to update (PRESS 0 to return to MENU): ");

      while (contactId != 0)
      {
        var contact = _contactService.GetContact(contactId);

        if (contact != null)
        {
          UserInterface.ShowContact(contact);

          var name = AnsiConsole.Ask<string>("Update contact's name: ");
          var email = AnsiConsole.Ask<string>("Update contact's email: ");
          var phoneNumber = AnsiConsole.Ask<string>("Phone number (format: xxx-xxx-xxxx): ");

          var contactDto = new ContactDto { Name = name, PhoneNumber = phoneNumber, Email = email };

          _contactService.UpdateContact(contactId, contactDto);
          break;
        }

        contactId = AnsiConsole.Ask<int>("Enter the contact's Id you want to update (PRESS 0 to return to MENU): ");
      }
    }

    public static void DeleteContact()
    {
      var result = _contactService.GetAllContacts();

      UserInterface.ShowContacts(result);

      var contactId = AnsiConsole.Ask<int>("Enter the contact's id you want to delete (PRESS 0 to return to MENU): ");

      while (contactId != 0)
      {
        _contactService.DeleteContact(contactId);

        contactId = AnsiConsole.Ask<int>("Enter the contact's id you want to delete (PRESS 0 to return to MENU): ");
      }
    }
    public static void ViewContact()
    {
      var contactId = AnsiConsole.Ask<int>("Enter the contact's Id you want to get (PRESS 0 to return to MENU): ");

      while (contactId != 0)
      {
        var contact = _contactService.GetContact(contactId);

        if (contact != null)
        {
          UserInterface.ShowContact(contact);
          Helper.ContinueMessage();
          break;
        }

        contactId = AnsiConsole.Ask<int>("Enter the contact's Id you want to get (PRESS 0 to return to MENU): ");
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
