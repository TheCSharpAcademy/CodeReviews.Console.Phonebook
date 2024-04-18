using PhoneBook.BBualdo.Services;
using PhoneBookLibrary;
using Spectre.Console;

namespace PhoneBook.BBualdo;

internal class AppEngine
{
  public bool IsRunning;

  public AppEngine()
  {
    IsRunning = true;
  }

  internal void MainMenu()
  {
    AnsiConsole.Clear();
    ConsoleEngine.ShowTitle();

    List<string> choices = [
      "Manage Contacts",
      "Manage Groups",
      "Quit"
    ];

    string choice = ConsoleEngine.GetChoiceOption("What would you like to do?", choices);

    switch (choice)
    {
      case "Manage Contacts":
        ContactsMenu();
        break;
      case "Manage Groups":
        GroupsMenu();
        break;
      case "Quit":
        AnsiConsole.Markup("[cyan1]Goodbye![/]\n");
        IsRunning = false;
        break;
    }
  }

  internal void ContactsMenu()
  {
    AnsiConsole.Clear();
    ConsoleEngine.ShowTitle();

    List<string> choices = [
      "Back",
      "Show Contacts",
      "Create Contact",
      "Update Contact",
      "Delete Contact"
      ];

    string choice = ConsoleEngine.GetChoiceOption("", choices);

    switch (choice)
    {
      case "Back":
        return;
      case "Show Contacts":
        ContactsService.ShowContacts();
        PressAnyKey();
        break;
      case "Create Contact":
        ContactsService.CreateContact();
        PressAnyKey();
        break;
      case "Update Contact":
        ContactsService.UpdateContact();
        PressAnyKey();
        break;
      case "Delete Contact":
        ContactsService.DeleteContact();
        PressAnyKey();
        break;
    }
  }

  internal void GroupsMenu()
  {
    AnsiConsole.Clear();
    ConsoleEngine.ShowTitle();

    List<string> choices = [
      "Back",
      "Show Groups",
      "Create Group",
      "Update Group",
      "Delete Group"
      ];

    string choice = ConsoleEngine.GetChoiceOption("", choices);

    switch (choice)
    {
      case "Back":
        return;
      case "Show Groups":
        GroupsService.ShowGroups();
        PressAnyKey();
        break;
      case "Create Group":
        GroupsService.CreateGroup();
        PressAnyKey();
        break;
      case "Update Group":
        GroupsService.UpdateGroup();
        PressAnyKey();
        break;
      case "Delete Group":
        GroupsService.DeleteGroup();
        PressAnyKey();
        break;
    }
  }

  private void PressAnyKey()
  {
    AnsiConsole.Markup("\n[cyan1]Press any key to continue.[/]");
    Console.ReadKey();
  }
}