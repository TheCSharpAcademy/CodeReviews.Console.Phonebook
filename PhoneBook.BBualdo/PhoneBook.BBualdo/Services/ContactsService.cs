using PhoneBookLibrary;
using PhoneBookLibrary.Controllers;
using PhoneBookLibrary.Models;

namespace PhoneBook.BBualdo.Services;

public static class ContactsService
{
  public static void CreateContact()
  {
    string name = UserInput.GetContactName();
    if (name == "0") return;
    string? email = UserInput.GetEmail(name);
    if (email == "0") return;

    string phoneNumber = UserInput.GetPhoneNumber(name);
    if (phoneNumber == "0") return;

    List<Group>? groups = GroupsController.GetGroups();
    int? groupId = null;

    if (groups != null)
    {
      ConsoleEngine.ShowGroupsTable(groups);
      groupId = UserInput.OptionallyGetGroupId();
      if (groupId == 0) return;
    }

    Contact contact = new() { Name = name, Email = email, PhoneNumber = phoneNumber, GroupId = groupId };

    ContactsController.InsertContact(contact);
  }

  public static bool ShowContacts()
  {
    List<Contact>? contacts = ContactsController.GetAllContacts();

    if (contacts == null) return false;

    ConsoleEngine.ShowContactsTable(contacts);
    return true;
  }

  public static void UpdateContact()
  {
    if (ShowContacts())
    {
      int contactId = UserInput.GetId("contact");
      if (contactId == 0) return;

      Contact? contact = ContactsController.GetContactById(contactId);
      if (contact == null) return;

      string name = UserInput.GetContactName(contact.Name);
      if (name == "0") return;
      string? email = UserInput.GetEmail(name);
      if (email == "0") return;
      string phoneNumber = UserInput.GetPhoneNumber(name);
      if (phoneNumber == "0") return;

      List<Group>? groups = GroupsController.GetGroups();
      int? groupId = null;

      if (groups != null)
      {
        ConsoleEngine.ShowGroupsTable(groups);
        groupId = UserInput.OptionallyGetGroupId();
        if (groupId == 0) return;
        if (groupId == null) groupId = contact.GroupId;
      }

      contact.Name = name;
      contact.Email = email;
      contact.PhoneNumber = phoneNumber;
      contact.GroupId = groupId;

      ContactsController.UpdateContact(contact);
    }
  }

  public static void DeleteContact()
  {
    if (ShowContacts())
    {
      int contactId = UserInput.GetId("contact");
      if (contactId == 0) return;
      Contact? contact = ContactsController.GetContactById(contactId);
      if (contact == null) return;

      ContactsController.DeleteContact(contact);
    }
  }
}