using Phonebook.kwm0304.Interfaces;
using Phonebook.kwm0304.Models;
using Phonebook.kwm0304.Utils;
using Phonebook.kwm0304.Views;
using Spectre.Console;

namespace Phonebook.kwm0304.Services;

public class ContactService(IContactRepository repository, GroupService service)
{
  private readonly IContactRepository _repository = repository;
  private readonly GroupService _groupService = service;

  public async Task CreateContact()
  {
    string name = UserPrompts.StringPrompt("name");
    string number = Helper.GetInput("number", Validation.IsValidPhoneNumber);
    string email = Helper.GetInput("email", Validation.IsValidEmail);
    string numberStr = Validation.FormatContactNumberStr(number);
    int numberInt = Validation.NormalizePhoneNumberInt(number);
    ContactGroup? group = await _groupService.GetGroup();
    var contact = new Contact
    {
      ContactName = name,
      ContactEmail = email,
      ContactPhoneStr = numberStr,
      ContactPhoneInt = numberInt,
      Group = group
    };
    await _repository.AddContact(contact);
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
        await _repository.UpdateContact(contact);
        break;
      case "Number":
        string number = Helper.GetInput("number", Validation.IsValidPhoneNumber);
        string newNumberString = Validation.FormatContactNumberStr(number);
        int newNumber = Validation.NormalizePhoneNumberInt(number);
        contact.ContactPhoneInt = newNumber;
        contact.ContactPhoneStr = newNumberString;
        await _repository.UpdateContact(contact);
        break;
      case "Group":
        ContactGroup? group = await _groupService.GetGroup();
        contact.Group = group;
        await _repository.UpdateContact(contact);
        break;
      case "Back":
        return;
      default:
        return;
    }
  }
}