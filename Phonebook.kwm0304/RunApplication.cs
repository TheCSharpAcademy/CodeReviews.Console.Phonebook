using Phonebook.kwm0304.Models;
using Phonebook.kwm0304.Services;
using Phonebook.kwm0304.Views;
using Spectre.Console;

namespace Phonebook.kwm0304;

public class RunApplication
{
  private readonly ContactService _contactService;
  public RunApplication(ContactService contactService)
  {
    _contactService = contactService;
  }
  public async Task OnStart()
  {
    while (true)
    {
      Console.Clear();
      string option = SelectionMenu.InitialSelection();
      switch (option)
      {
        case "Add contact":
          await _contactService.CreateContact();
          break;
        case "View contacts":
          await _contactService.HandleViewContacts();
          break;
        case "Exit":
          AnsiConsole.WriteLine("Goodbye!");
          Environment.Exit(0);
          break;
        default:
          break;
      }
    }
  }
}