using Spectre.Console;
using Phonebook.yemiodetola.Models;

namespace Phonebook.yemiOdetola;

public class ContactController
{

  private readonly ContactsRepository _contactsRepository;
  public ContactController(ContactsRepository contactsRepository)
  {
    _contactsRepository = contactsRepository;
  }
  public async Task AddContact()
  {
    Contact contact = new Contact();
    contact.Name = AnsiConsole.Ask<string>("FullName");
    bool isValidEmail = false;
    bool isValidPhoneNumber = false;

    while (!isValidEmail)
    {
      string email = AnsiConsole.Ask<string>("Enter email address: ");
      isValidEmail = contact.validateEmailAddress(email);
      if (!isValidEmail)
      {
        AnsiConsole.MarkupLine("[red]Invalid email address. Please try again.[/]");
      }
      else
      {
        contact.Email = email;
      }
    }

    while (!isValidPhoneNumber)
    {
      string phoneNumber = AnsiConsole.Ask<string>("Enter phone number (ex: 09137121527): ");
      isValidPhoneNumber = contact.validatePhoneNumber(phoneNumber);
      if (!isValidPhoneNumber)
      {
        AnsiConsole.MarkupLine("[red]Incorrect phone number. Please try again.[/]");
      }
      else
      {
        contact.PhoneNumber = phoneNumber;
      }
    }

    try
    {
      await _contactsRepository.AddContact(contact);
      AnsiConsole.MarkupLine("[bold green]Contact added successfully![/]");
    }
    catch (Exception e)
    {
      AnsiConsole.MarkupLine($"[red]Error adding contact: {e.Message}[/]");
    }

  }
  public async Task DeleteContact()
  {
    string name = AnsiConsole.Ask<string>("Enter name of contact to delete: ");
    try
    {
      await _contactsRepository.DeleteContact(name);
      AnsiConsole.MarkupLine("[bold green]Contact deleted successfully![/]");
    }
    catch (Exception ex)
    {
      AnsiConsole.MarkupLine($"[red]Error deleting contact: {ex.Message}[/]");
    }

  }
  public async Task UpdateContact()
  {
    string name = AnsiConsole.Ask<string>("Enter name of contact to update: ");
    try
    {
      await _contactsRepository.UpdateContact(name);
      AnsiConsole.MarkupLine("[bold green]Contact updated successfully![/]");
    }
    catch (Exception ex)
    {
      AnsiConsole.MarkupLine($"[red]Error updating contact: {ex.Message}[/]");
    }
  }
  public async Task GetAllContacts()
  {
    try
    {
      List<Contact>? contacts = await _contactsRepository.GetContacts();
      if (contacts.Count == 0)
      {
        AnsiConsole.MarkupLine("[bold red]No contacts found![/]");
        return;
      }
      Table table = new Table()
        .Title("[bold red]Contacts list[/]")
        .AddColumn("[bold yellow]Id[/]")
        .AddColumn("[bold darkorange]Name[/]")
        .AddColumn("[bold darkorange]Email[/]")
        .AddColumn("[bold darkorange]Phone number[/]");
      foreach (Contact contact in contacts)
      {
        table.AddRow(
          $"[bold yellow]{contacts.IndexOf(contact) + 1}[/]",
          $"[bold darkorange]{contact.Name}[/]",
          $"[bold darkorange]{contact.Email}[/]",
          $"[bold darkorange]{contact.PhoneNumber}[/]"
        );
      }
      AnsiConsole.Write(table);
    }
    catch (Exception ex)
    {
      AnsiConsole.MarkupLine($"[red]Error getting contacts: {ex.Message}[/]");
    }
  }
}

