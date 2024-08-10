using Phonebook.kwm0304.Enums;
using Phonebook.kwm0304.Interfaces;
using Phonebook.kwm0304.Models;
using Phonebook.kwm0304.Utils;
using Phonebook.kwm0304.Views;
using Spectre.Console;

namespace Phonebook.kwm0304.Services;

public class ContactService(IContactRepository repository, GroupService service, EmailService emailService, SmsHandler handler)
{
  private readonly IContactRepository _repository = repository;
  private readonly GroupService _groupService = service;
  private readonly EmailService _emailService = emailService;
  private readonly SmsHandler _smsHandler = handler;

  public async Task CreateContact()
  {
    string name = UserPrompts.StringPrompt("name");
    string number = Helper.GetInput("number", Validation.IsValidPhoneNumber);
    string email = Helper.GetInput("email", Validation.IsValidEmail);
    string numberStr = Validation.FormatContactNumberStr(number);
    long numberInt = Validation.NormalizePhoneNumberInt(number);
    try
    {
      ContactGroup? group = await _groupService.GetGroup();
      var contact = new Contact
      {
        ContactName = name,
        ContactEmail = email,
        ContactPhoneStr = numberStr,
        ContactPhoneInt = numberInt,
        Group = group
      };
      try
      {
        await _repository.AddContact(contact);
      }
      catch (Exception e)
      {
        AnsiConsole.WriteLine($"Error adding contact: {e.Message}");
      }
    }
    catch (Exception e)
    {
      AnsiConsole.WriteLine($"Error getting contact groups. {e.Message}");
      return;
    }
  }

  public async Task UpdateContact(Contact contact)
  {
    string choice = SelectionMenu.EditContactOptions();
    await HandleEdit(choice, contact);
  }

  private async Task HandleEdit(string choice, Contact contact)
  {
    switch (choice)
    {
      case "Name":
        string name = UserPrompts.StringPrompt("name");
        contact.ContactName = name;
        try
        {
          await _repository.UpdateContact(contact);
        }
        catch (Exception e)
        {
          AnsiConsole.WriteLine(e.Message);
          return;
        }
        break;
      case "Number":
        string number = Helper.GetInput("number", Validation.IsValidPhoneNumber);
        string newNumberString = Validation.FormatContactNumberStr(number);
        long newNumber = Validation.NormalizePhoneNumberInt(number);
        contact.ContactPhoneInt = newNumber;
        contact.ContactPhoneStr = newNumberString;
        try
        {
          await _repository.UpdateContact(contact);
        }
        catch (Exception e)
        {
          AnsiConsole.WriteLine(e.Message);
          return;
        }
        break;
      case "Group":
        ContactGroup? group = await _groupService.GetGroup();
        contact.Group = group;
        try
        {
          await _repository.UpdateContact(contact);
        }
        catch (Exception e)
        {
          AnsiConsole.WriteLine(e.Message);
          return;
        }
        break;
      case "Email":
        string newEmail = Helper.GetInput("email", Validation.IsValidEmailAddress);
        contact.ContactEmail = newEmail;
        try
        {
          await _repository.UpdateContact(contact);
        }
        catch (Exception e)
        {
          AnsiConsole.WriteLine(e.Message);
          return;
        }
        break;
      case "Back":
        return;
      default:
        return;
    }
  }

  public async Task HandleViewContacts()
  {
    Contact contact = await ChooseContact();
    if (contact.ContactName == "Back")
    {
      return;
    }
    ContactOption option = SelectionMenu.SelectContactOption();
    switch (option)
    {
      case ContactOption.EmailContact:
        if (contact.ContactEmail != null)
        {
          try
          {
            await _emailService.CreateMessage(contact)!;
          }
          catch (Exception e)
          {
            AnsiConsole.WriteLine(e.Message);
            return;
          }

        }
        else
        {
          AnsiConsole.WriteLine("No email saved for this contact");
          return;
        }
        break;
      case ContactOption.TextContact:
        try
        {
          await _smsHandler.SendSms(contact);
        }
        catch (Exception e)
        {
          AnsiConsole.WriteLine(e.Message);
          return;
        }
        break;
      case ContactOption.EditContact:
        await UpdateContact(contact);
        break;
      case ContactOption.DeleteContact:
        bool confirmDelete = AnsiConsole.Confirm(
          $"Are you sure you want to delete\n{contact.ContactName}\n{contact.ContactPhoneStr}");
        if (confirmDelete)
        {
          try
          {
            await _repository.DeleteContact(contact);
          }
          catch (Exception e)
          {
            AnsiConsole.WriteLine(e.Message);
            return;
          }
        }
        break;
      case ContactOption.Back:
        return;
    }
  }

  public async Task<Contact> ChooseContact()
  {
    try
    {
      List<Contact> contacts = await _repository.GetAllContactsAsync();
      return SelectionMenu.SelectContact(contacts);
    }
    catch (Exception e)
    {
      AnsiConsole.WriteLine(e.Message);
      return default!;
    }
  }
}