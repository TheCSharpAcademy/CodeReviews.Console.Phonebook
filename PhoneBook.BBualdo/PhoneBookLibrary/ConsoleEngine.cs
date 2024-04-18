using PhoneBookLibrary.Models;
using Spectre.Console;

namespace PhoneBookLibrary;

public static class ConsoleEngine
{
  public static string GetChoiceOption(string title, List<string> choices)
  {
    SelectionPrompt<string> prompt = new SelectionPrompt<string>()
                                    .Title(title)
                                    .AddChoices(choices);

    prompt.HighlightStyle = new Style(Color.Cyan1);

    string choice = AnsiConsole.Prompt(prompt);

    return choice;
  }

  public static void ShowGroupsTable(List<Group> groups)
  {
    Table table = new Table().Title("Contact Groups");
    table.AddColumn(new TableColumn("[mediumorchid1]ID[/]"));
    table.AddColumn(new TableColumn("[mediumorchid1]Name[/]"));
    table.AddColumn(new TableColumn("[mediumorchid1]Contacts[/]"));

    foreach (Group group in groups)
    {
      table.AddRow(group.GroupId.ToString(), group.Name, GetContactsAsString(group.Contacts));
    }

    AnsiConsole.Write(table);
  }

  public static void ShowContactsTable(List<Contact> contacts)
  {
    Table table = new Table().Title("Contacts");
    table.AddColumn(new TableColumn("[mediumorchid1]ID[/]"));
    table.AddColumn(new TableColumn("[mediumorchid1]Name[/]"));
    table.AddColumn(new TableColumn("[mediumorchid1]Email[/]"));
    table.AddColumn(new TableColumn("[mediumorchid1]Phone Number[/]"));
    table.AddColumn(new TableColumn("[mediumorchid1]Group[/]"));

    foreach (Contact contact in contacts)
    {
      table.AddRow(contact.ContactId.ToString(), contact.Name, contact.Email ?? "", contact.PhoneNumber, contact.Group?.Name ?? "");
    }

    AnsiConsole.Write(table);
  }

  public static void ShowTitle()
  {
    Rule rule = new Rule("Phone Book").RoundedBorder().Centered();
    rule.Style = new Style(Color.MediumOrchid1);

    AnsiConsole.Write(rule);
  }

  private static string GetContactsAsString(List<Contact> contacts)
  {
    if (contacts == null || contacts.Count == 0)
    {
      return "[red]No contacts[/]";
    }

    return string.Join("\n", contacts.Select(contact => contact.Name));
  }
}