using PhoneBook.BBualdo.Helpers;
using Spectre.Console;

namespace PhoneBook.BBualdo;

public class UserInput
{
  public static string GetGroupName(string name = "group of contacts")
  {
    string groupName = AnsiConsole.Ask<string>($"[mediumorchid1]Enter new name for a [cyan1]{name}[/][/] or type 0 to go back: ");

    if (groupName == "0") return "0";

    while (!GroupNameValidator.IsValid(groupName))
    {
      groupName = AnsiConsole.Ask<string>("[cyan1]Try again: [/]");
      if (groupName == "0") return "0";
    }

    return groupName;
  }

  public static string GetContactName(string name = "a contact")
  {
    string contactName = AnsiConsole.Ask<string>($"[mediumorchid1]Enter new name for [cyan1]{name}[/][/] or type 0 to go back: ");

    if (contactName == "0") return "0";

    while (!ContactNameValidator.IsValid(contactName))
    {
      contactName = AnsiConsole.Ask<string>("[yellow]Try again: [/]");
      if (contactName == "0") return "0";
    }

    return contactName;
  }

  public static int GetId(string nameOfEntity)
  {
    int groupId = AnsiConsole.Ask<int>($"[mediumorchid1]Enter ID of {nameOfEntity.ToLower()} you want to interact with[/] or type 0 to go back:");

    if (groupId == 0) return 0;

    return groupId;
  }

  public static int? OptionallyGetGroupId()
  {
    string? groupId = AnsiConsole.Prompt(
      new TextPrompt<string?>("[mediumorchid1]Enter ID of group you want to place your contact in[/], leave blank if you don't want to add it to any or type 0 to go back: ")
      .AllowEmpty());

    if (groupId == "0") return 0;
    if (string.IsNullOrEmpty(groupId)) return null;

    while (!OptionalIdValidator.IsValid(groupId))
    {
      groupId = AnsiConsole.Prompt(
        new TextPrompt<string>("[yellow]Try again: [/]")
        .AllowEmpty());
      if (groupId == "0") return 0;
      if (string.IsNullOrEmpty(groupId)) return null;
    }

    return Convert.ToInt32(groupId);
  }

  public static string? GetEmail(string name)
  {
    string? email = AnsiConsole.Prompt(
      new TextPrompt<string?>($"[mediumorchid1]Enter new email for [cyan1]{name}[/][/], leave empty or type 0 to go back: ")
      .AllowEmpty());

    if (email == "0") return "0";
    if (string.IsNullOrEmpty(email)) return null;

    while (!EmailValidator.IsValid(email))
    {
      email = AnsiConsole.Ask<string>("[yellow]Try again: [/]");
      if (email == "0") return "0";
      if ((string.IsNullOrEmpty(email))) return null;
    }

    return email;
  }

  public static string GetPhoneNumber(string name)
  {
    string phoneNumber = AnsiConsole.Ask<string>($"[mediumorchid1]Enter new phone number [cyan1](Format: 000-000-000)[/][/] for [cyan1]{name}[/] or type 0 to go back: ");

    if (phoneNumber == "0") return "0";

    while (!PhoneNumberValidator.IsValid(phoneNumber))
    {
      phoneNumber = AnsiConsole.Ask<string>("[yellow]Try again: [/]");
      if (phoneNumber == "0") return "0";
    }

    return phoneNumber;
  }
}