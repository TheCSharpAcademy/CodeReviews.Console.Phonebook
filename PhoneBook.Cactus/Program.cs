using PhoneBook.Cactus;
using Spectre.Console;
enum MenuOptions
{
    AddContact,
    DeleteContact,
    UpdateContact,
    ViewContact,
    ViewAllContact,
    Quit
}

public static class Application
{
    public static void Main()
    {
        bool isAppRunning = true;
        while (isAppRunning)
        {
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<MenuOptions>()
            .Title("What would you like to do?")
            .AddChoices(
                MenuOptions.AddContact,
                MenuOptions.DeleteContact,
                MenuOptions.UpdateContact,
                MenuOptions.ViewAllContact,
                MenuOptions.ViewContact,
                MenuOptions.Quit));

            switch (option)
            {
                case MenuOptions.AddContact:
                    ContactService.InsertContact();
                    break;
                case MenuOptions.DeleteContact:
                    ContactService.DeleteContact();
                    break;
                case MenuOptions.UpdateContact:
                    ContactController.UpdateContact();
                    break;
                case MenuOptions.ViewContact:
                    ContactService.ShowSpecificContact();
                    break;
                case MenuOptions.ViewAllContact:
                    ContactService.ShowAllContacts();
                    break;
                case MenuOptions.Quit:
                    isAppRunning = false;
                    break;
            }
        }
    }
}