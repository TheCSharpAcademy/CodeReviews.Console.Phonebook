using Phonebook.kwm0304.Enums;
using Phonebook.kwm0304.Models;

namespace Phonebook.kwm0304.Views;

public class SelectionMenu
{
  public static Contact SelectContact(List<Contact> contacts)
  {
    var cancel = new Contact { ContactName = "Back" };
    var menu = new Container<Contact>("[bold chartreuse2_1]Select contact:[/] \n\n", contacts, cancel);
    return menu.Show()!;
  }

  public static ContactGroup SelectGroup(List<ContactGroup> groups)
  {
    var cancel = new ContactGroup { GroupName = "Back" };
    var menu = new Container<ContactGroup>("[bold chartreuse2_1]Select group:[/] \n\n", groups, cancel);
    return menu.Show()!;
  }

  public static string InitialSelection()
  {
    string cancel = "Back";
    var menuOptions = new List<string> { "Add contact", "View contacts" };
    var menu = new Container<string>("[bold chartreuse2_1]What would you like to do?[/]", menuOptions, cancel);
    return menu.Show()!;
  }

  public static ContactOption SelectContactOption()
  {
    List<ContactOption> options = [ContactOption.TextContact, ContactOption.EmailContact, ContactOption.EditContact, ContactOption.DeleteContact];
    var menu = new Container<ContactOption>("[bold chartreuse2_1]What would you like to do?[/]", options, ContactOption.Back);
    return menu.Show();
  }
  
  public static string EditContactOptions()
  {
    var menuOptions = new List<string> { "Name", "Number", "Group", "Email", "Back" };
    var menu = new Container<string>("What would you like to change?", menuOptions, "Cancel");
    return menu.Show()!;
  }

  public static string ContactGroupOption()
  {
    var menuOptions = new List<string> { "Add group", "Choose existing" };
    var menu = new Container<string>("[bold chartreuse2_1]What would you like to do?[/]", menuOptions, "Cancel");
    return menu.Show()!;
  }
}