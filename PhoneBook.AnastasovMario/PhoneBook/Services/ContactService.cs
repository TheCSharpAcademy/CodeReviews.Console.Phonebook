using PhoneBook.Contracts;
using PhoneBook.Data.Models;
using PhoneBook.Helpers;
using Spectre.Console;

namespace PhoneBook.Services
{
  public class ContactService(PhonebookDbContext context) : IContactService
  {
    private readonly PhonebookDbContext _context = context;

    public void AddContact(Contact contact)
    {
      bool emailExists = _context.Contacts.FirstOrDefault(c => c.Email == contact.Email) != null;

      if (emailExists)
      {
        throw new Exception("The email is already in use.");
      }

      bool phoneExists = _context.Contacts.FirstOrDefault(c => c.PhoneNumber == contact.PhoneNumber) != null;

      if (phoneExists)
      {
        throw new Exception("The phone number already exists.");
      }

      _context.Contacts.Add(new()
      {
        Name = contact.Name,
        PhoneNumber = contact.PhoneNumber,
        Email = contact.Email,
      });

      _context.SaveChanges();

    }

    public void DeleteContact(string email)
    {
      var contact = _context.Contacts.FirstOrDefault(c => c.Email == email);

      if (contact == null)
      {
        throw new Exception("\nContact with this email doesn't exist");
      }

      _context.Contacts.Remove(contact);
      _context.SaveChanges();

      Console.WriteLine("\nContact has been deleted successfully");
    }

    public List<Contact> GetAllContacts()
    {
      return [.. _context.Contacts];
    }

    public Contact GetContact(string email)
    {
      var contact = _context.Contacts.FirstOrDefault(c => c.Email == email);

      if (contact == null)
      {
        throw new Exception("\nContact with this email doesn't exist.");
      }

      return contact;
    }

    public void UpdateContact(string email, Contact updatedContact)
    {
      var contact = _context.Contacts.First(c => c.Email == email);
      contact.Name = updatedContact.Name;
      contact.PhoneNumber = updatedContact.PhoneNumber;
      contact.Email = updatedContact.Email;

      _context.SaveChangesAsync();
    }

    public Contact GetValidatedContact()
    {
      string name = AnsiConsole.Ask<string>("Enter the contact's name: ");
      string email = AnsiConsole.Ask<string>("Enter the contact's email: ");

      while (!Validator.IsEmailValid(email))
      {
        AnsiConsole.MarkupLine("[red]Invalid email format![/]");
        if (!Helper.PromptToContinue()) return null; // Stop if the user chooses not to continue
        email = AnsiConsole.Ask<string>("Enter the contact's email: ");
      }

      string phoneNumber = AnsiConsole.Ask<string>("Phone number (format: xxx-xxx-xxxx): ");

      while (!Validator.IsPhoneNumberValid(phoneNumber))
      {
        AnsiConsole.MarkupLine("[red]Invalid phone number format! Should be xxx-xxx-xxxx[/]");
        if (!Helper.PromptToContinue()) return null; // Stop if the user chooses not to continue
        phoneNumber = AnsiConsole.Ask<string>("Phone number (format: xxx-xxx-xxxx): ");
      }

      return new Contact { Name = name, Email = email, PhoneNumber = phoneNumber };
    }
  }
}
