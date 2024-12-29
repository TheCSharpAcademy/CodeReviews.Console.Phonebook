// See https://aka.ms/new-console-template for more information
using PhoneBook;
using PhoneBook.Data.Models;
using PhoneBook.Models.Enums;
using Spectre.Console;

using var db = new PhonebookDbContext();
var isAppRunning = true;
while (isAppRunning)
{
  var option = AnsiConsole.Prompt(
      new SelectionPrompt<MenuOptions>()
      .Title("\nWhat would you like to do?")
      .AddChoices(
          MenuOptions.AddContact,
          MenuOptions.DeleteContact,
          MenuOptions.UpdateContact,
          MenuOptions.ViewContact,
          MenuOptions.ViewAllContacts,
          MenuOptions.Quit));

  switch (option)
  {
    case MenuOptions.AddContact:
      PhoneBookController.AddContact();
      break;
    case MenuOptions.DeleteContact:
      PhoneBookController.DeleteContact();
      break;
    case MenuOptions.UpdateContact:
      PhoneBookController.UpdateContact();
      break;
    case MenuOptions.ViewContact:
      PhoneBookController.ViewContact();
      break;
    case MenuOptions.ViewAllContacts:
      PhoneBookController.ViewAllContacts();
      break;
    case MenuOptions.Quit:
      isAppRunning = false;
      Console.WriteLine("\nGoodbye!\n");
      Environment.Exit(0);
      break;
    default:
      break;
  }

}