using Phonebook.kwm0304.Models;
using Phonebook.kwm0304.Services;
using Phonebook.kwm0304.Views;
using Spectre.Console;

namespace Phonebook.kwm0304;

public class RunApplication
{
  private readonly ContactService _contactService;
  public string header = @"
.______    __    __    ______   .__   __.  _______ .______     ______     ______    __  ___ 
|   _  \  |  |  |  |  /  __  \  |  \ |  | |   ____||   _  \   /  __  \   /  __  \  |  |/  / 
|  |_)  | |  |__|  | |  |  |  | |   \|  | |  |__   |  |_)  | |  |  |  | |  |  |  | |  '  /  
|   ___/  |   __   | |  |  |  | |  . `  | |   __|  |   _  <  |  |  |  | |  |  |  | |    <   
|  |      |  |  |  | |  `--'  | |  |\   | |  |____ |  |_)  | |  `--'  | |  `--'  | |  .  \  
| _|      |__|  |__|  \______/  |__| \__| |_______||______/   \______/   \______/  |__|\__\ 
                                                                                            
";
  public RunApplication(ContactService contactService)
  {
    _contactService = contactService;
  }
  public async Task ClearAndWriteHeader()
  {
    Console.Clear();
    AnsiConsole.WriteLine(header);
    await Task.Delay(50);
  }
  public async Task OnStart()
  {
    while (true)
    {
      await ClearAndWriteHeader();
      string option = SelectionMenu.InitialSelection();
      switch (option)
      {
        case "Add contact":
          await _contactService.CreateContact();
          break;
        case "View contacts":
          await _contactService.HandleViewContacts();
          break;
        case "Back":
          AnsiConsole.WriteLine("Goodbye!");
          Environment.Exit(0);
          break;
        default:
          break;
      }
    }
  }
}