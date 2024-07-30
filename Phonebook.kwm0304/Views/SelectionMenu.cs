using Phonebook.kwm0304.Enums;
using Phonebook.kwm0304.Models;

namespace Phonebook.kwm0304.Views;

public class SelectionMenu
{
  //select contact to view 
  public static Contact SelectContact(List<Contact> contacts)
  {
    var menu = new Container<Contact>("[bold chartreuse2_1]Select contact:[/] \n\n", contacts);
    return menu.Show()!;
  }
  //select group
  public static ContactGroup SelectGroup(List<ContactGroup> groups)
  {
    var menu = new Container<ContactGroup>("[bold chartreuse2_1]Select group:[/] \n\n", groups);
    return menu.Show()!;
  }
  //main menu crud
  

  public static string InitialSelection()
  {
    var menuOptions = new List<string> { "Add contact", "View contacts", "Exit" };
    var menu = new Container<string>("[bold chartreuse2_1]What would you like to do?[/]", menuOptions);
    return menu.Show()!;
  }

  public static ContactOption SelectContactOption()
  {
    List<ContactOption> options = [ContactOption.TextContact, ContactOption.EmailContact, ContactOption.EditContact, ContactOption.DeleteContact];
    var menu = new Container<ContactOption>("[bold chartreuse2_1]What would you like to do?[/]", options);
    return menu.Show();
  }
  //if edit contact
  public static string EditContactOptions()
  {
    var menuOptions = new List<string> { "Name", "Number", "Group", "Back" };
    var menu = new Container<string>("What would you like to change?", menuOptions);
    return menu.Show()!;
  }

  public static string ContactGroupOption()
  {
    var menuOptions = new List<string> { "Add group", "Choose existing" };
    var menu = new Container<string>("[bold chartreuse2_1]What would you like to do?[/]", menuOptions);
    return menu.Show()!;
  }
}