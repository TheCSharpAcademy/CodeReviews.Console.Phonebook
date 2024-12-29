using Spectre.Console;

namespace PhoneBook.Helpers
{
  public static class Helper
  {
    public static void ContinueMessage()
    {
      Console.WriteLine("Enter any key to continue");
      Console.ReadLine();
    }

    public static bool PromptToContinue()
    {
      var choice = AnsiConsole.Prompt(
          new SelectionPrompt<string>()
              .Title("[yellow]Do you want to try again or exit?[/]")
              .AddChoices("Try again", "Exit"));

      return choice == "Try again";
    }
  }
}
