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
    contact.Email = Validation.GetAndValidateEmail();
    contact.PhoneNumber = Validation.GetAndValidatePhone();
    int? categoryId = await SelectContactCategory();
    Console.WriteLine($"CategoryID {categoryId.Value}");
    if (categoryId == null)
    {
      AnsiConsole.MarkupLine("[red]Operation canceled: No valid category selected.[/]");
      return;
    }

    contact.CategoryId = categoryId.Value;

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

  public async Task<int?> SelectContactCategory()
  {
    var categories = await _contactsRepository.GetCategories();
    if (!categories.Any())
    {
      AnsiConsole.MarkupLine("[bold red]No categories was found![/]");
      return null;
    }

    var categoryNames = categories.Select(x => x.Name).ToArray();
    var categoryOption = AnsiConsole.Prompt<string>(new SelectionPrompt<string>()
        .Title("Select category")
        .AddChoices(categoryNames));
    var category = categories.SingleOrDefault(x => x.Name == categoryOption);

    return category.Id;
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
    Contact? contact = null;

    try
    {
      contact = await _contactsRepository.GetContactByName(name);
    }
    catch (Exception ex)
    {
      AnsiConsole.MarkupLine($"[red]Error getting contact: {ex.Message}[/]");
      return;
    }

    if (contact == null)
    {
      AnsiConsole.MarkupLine("[bold red]Contact not found![/]");
      return;
    }

    var newName = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter new name (current: [bold]{contact.Name}[/]):")
                .AllowEmpty()
        );
    var newEmail = AnsiConsole.Prompt(
        new TextPrompt<string>($"Enter new email (current: [bold]{contact.Email}[/]):")
            .AllowEmpty()
    );
    var newPhoneNumber = AnsiConsole.Prompt(
        new TextPrompt<string>($"Enter new phone number (current: [bold]{contact.PhoneNumber}[/]):")
            .AllowEmpty()
    );
    contact.Name = string.IsNullOrEmpty(newName) ? contact.Name : newName;
    contact.Email = string.IsNullOrEmpty(newEmail) ? contact.Email : newEmail;
    contact.PhoneNumber = string.IsNullOrEmpty(newPhoneNumber) ? contact.PhoneNumber : newPhoneNumber;

    try
    {
      await _contactsRepository.UpdateContact(contact);
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
        .AddColumn("[bold darkorange]Phone number[/]")
        .AddColumn("[bold darkorange]Group[/]");
      foreach (Contact contact in contacts)
      {
        table.AddRow(
          $"[bold yellow]{contacts.IndexOf(contact) + 1}[/]",
          $"[bold darkorange]{contact.Name}[/]",
          $"[bold darkorange]{contact.Email}[/]",
          $"[bold darkorange]{contact.PhoneNumber}[/]",
          $"[bold darkorange]{contact.Category.Name}[/]"
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

