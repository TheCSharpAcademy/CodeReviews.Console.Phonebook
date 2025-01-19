using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using Phonebook.yemiodetola.Models;


namespace Phonebook.yemiOdetola;

public class ContactsRepository
{

  public async Task AddContact(Contact contact)
  {
    using (var db = new PhonebookContext())
    {
      try
      {
        db.Contacts.Add(contact);
        await db.SaveChangesAsync();
        AnsiConsole.WriteLine("Contact added successfully.");
      }
      catch (Exception ex)
      {
        AnsiConsole.WriteLine($"Error: {ex.Message}");
      }
    }
    AnsiConsole.WriteLine($"{contact.Name} is added");
  }

  public async Task<List<Contact>> GetContacts()
  {
    using (var db = new PhonebookContext())
    {
      return await db.Contacts
      .Include(c => c.Category)
      .ToListAsync();
    }
  }

  public async Task<Contact> GetContactByName(string name)
  {
    using (var db = new PhonebookContext())
    {
      var contact = await db.Contacts.FirstOrDefaultAsync(c => c.Name == name);
      if (contact == null)
      {
        throw new InvalidOperationException($"Contact with Name {name} not found.");
      }
      return contact;
    }
  }

  public async Task UpdateContact(string name)
  {
    using (var db = new PhonebookContext())
    {
      var contact = await GetContactByName(name);
      var newName = AnsiConsole.Ask<string>("Enter new name: "); ;
      var newEmail = AnsiConsole.Ask<string>("Enter new email: ");
      var newPhoneNumber = AnsiConsole.Ask<string>("Enter new phone number: ");
      if (!string.IsNullOrEmpty(newName)) contact.Name = newName;
      if (!string.IsNullOrEmpty(newEmail)) contact.Email = newEmail;
      if (!string.IsNullOrEmpty(newPhoneNumber)) contact.PhoneNumber = newPhoneNumber;
      db.Update(contact);
      await db.SaveChangesAsync();
    }
  }

  public async Task DeleteContact(string name)
  {
    using (var db = new PhonebookContext())
    {
      var contact = await GetContactByName(name);
      if (contact == null)
      {
        throw new InvalidOperationException($"Contact with Name {name} not found.");
      }
      db.Contacts.Remove(contact);
      await db.SaveChangesAsync();
    }
  }
  public async Task TestConnection()
  {
    try
    {
      using (var db = new PhonebookContext())
      {
        bool canConnect = await db.Database.CanConnectAsync();

        if (canConnect)
        {
          AnsiConsole.MarkupLine("[green]Successfully connected to database![/]");
        }
        else
        {
          AnsiConsole.MarkupLine("[red]Cannot connected to database![/]");
        }
      }
    }
    catch (Exception ex)
    {
      AnsiConsole.MarkupLine($"[red]Connection failed: {ex.Message}[/]");
    }
  }

  public async Task CreateCategory(Category category)
  {
    using (var db = new PhonebookContext())
    {
      try
      {
        db.Categories.Add(category);
        await db.SaveChangesAsync();
        AnsiConsole.WriteLine("Category added successfully.");
      }
      catch (Exception ex)
      {
        AnsiConsole.WriteLine($"Error: {ex.Message}");
      }
    }
  }

  public async Task<List<Category>> GetCategories()
  {
    using (var db = new PhonebookContext())
    {
      return await db.Categories.ToListAsync();
    }
  }
}
