using System.Text.RegularExpressions;
using Phonebook.wkktoria.Controllers;
using Phonebook.wkktoria.Enums;
using Spectre.Console;

namespace Phonebook.wkktoria;

public class UserInterface
{
    private readonly ContactController _contactController = new();

    public void Run()
    {
        MainMenu();
    }

    private void MainMenu()
    {
        var isAppRunning = true;

        while (isAppRunning)
        {
            Console.Clear();

            var option = GetOption(new List<string>
            {
                MainMenuOptions.ManageContacts.ToDescription(),
                MainMenuOptions.Quit.ToDescription()
            });

            switch (option)
            {
                case nameof(MainMenuOptions.ManageContacts):
                    ContactsMenu();
                    break;
                case nameof(MainMenuOptions.Quit):
                    isAppRunning = false;
                    break;
            }
        }
    }

    private void ContactsMenu()
    {
        var goBack = false;

        while (!goBack)
        {
            Console.Clear();

            var option = GetOption(new List<string>
            {
                ContactMenuOptions.AddContact.ToDescription(),
                ContactMenuOptions.UpdateContact.ToDescription(),
                ContactMenuOptions.DeleteContact.ToDescription(),
                ContactMenuOptions.ViewContactDetails.ToDescription(),
                ContactMenuOptions.ViewContacts.ToDescription(),
                ContactMenuOptions.GoBack.ToDescription()
            });

            switch (option)
            {
                case nameof(ContactMenuOptions.AddContact):
                    _contactController.AddContact();
                    PressAnyKeyToContinue();
                    break;
                case nameof(ContactMenuOptions.UpdateContact):
                    _contactController.UpdateContact();
                    PressAnyKeyToContinue();
                    break;
                case nameof(ContactMenuOptions.DeleteContact):
                    _contactController.DeleteContact();
                    PressAnyKeyToContinue();
                    break;
                case nameof(ContactMenuOptions.ViewContactDetails):
                    _contactController.ViewContactDetails();
                    PressAnyKeyToContinue();
                    break;
                case nameof(ContactMenuOptions.ViewContacts):
                    _contactController.ViewContacts();
                    PressAnyKeyToContinue();
                    break;
                case nameof(ContactMenuOptions.GoBack):
                    goBack = true;
                    break;
            }
        }
    }

    private static string GetOption(List<string> choices)
    {
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .AddChoices(choices)
        );

        return Regex.Replace(option, @"\s+", string.Empty);
    }

    private static void PressAnyKeyToContinue()
    {
        Console.Write("\nPress any key to continue...");
        Console.ReadKey();
    }
}