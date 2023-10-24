using System.Text.RegularExpressions;
using Phonebook.wkktoria.Enums;
using Phonebook.wkktoria.Services;
using Spectre.Console;

namespace Phonebook.wkktoria;

public partial class UserInterface
{
    private readonly CategoryService _categoryService = new();
    private readonly ContactService _contactService = new();

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
                MainMenuOptions.ManageCategories.ToDescription(),
                MainMenuOptions.Quit.ToDescription()
            });

            switch (option)
            {
                case nameof(MainMenuOptions.ManageContacts):
                    ContactsMenu();
                    break;
                case nameof(MainMenuOptions.ManageCategories):
                    CategoriesMenu();
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
                    _contactService.AddContact();
                    PressAnyKeyToContinue();
                    break;
                case nameof(ContactMenuOptions.UpdateContact):
                    _contactService.UpdateContact();
                    PressAnyKeyToContinue();
                    break;
                case nameof(ContactMenuOptions.DeleteContact):
                    _contactService.DeleteContact();
                    PressAnyKeyToContinue();
                    break;
                case nameof(ContactMenuOptions.ViewContactDetails):
                    _contactService.ViewContactDetails();
                    PressAnyKeyToContinue();
                    break;
                case nameof(ContactMenuOptions.ViewContacts):
                    _contactService.ViewContacts();
                    PressAnyKeyToContinue();
                    break;
                case nameof(ContactMenuOptions.GoBack):
                    goBack = true;
                    break;
            }
        }
    }

    private void CategoriesMenu()
    {
        var goBack = false;

        while (!goBack)
        {
            Console.Clear();

            var option = GetOption(new List<string>
            {
                CategoryMenuOptions.AddCategory.ToDescription(),
                CategoryMenuOptions.UpdateCategory.ToDescription(),
                CategoryMenuOptions.DeleteCategory.ToDescription(),
                CategoryMenuOptions.ViewContactsInCategory.ToDescription(),
                CategoryMenuOptions.ViewCategories.ToDescription(),
                CategoryMenuOptions.GoBack.ToDescription()
            });

            switch (option)
            {
                case nameof(CategoryMenuOptions.AddCategory):
                    _categoryService.AddCategory();
                    PressAnyKeyToContinue();
                    break;
                case nameof(CategoryMenuOptions.ViewContactsInCategory):
                    _categoryService.ViewContactsInCategory();
                    PressAnyKeyToContinue();
                    break;
                case nameof(CategoryMenuOptions.UpdateCategory):
                    _categoryService.UpdateCategory();
                    PressAnyKeyToContinue();
                    break;
                case nameof(CategoryMenuOptions.DeleteCategory):
                    _categoryService.DeleteCategory();
                    PressAnyKeyToContinue();
                    break;
                case nameof(CategoryMenuOptions.ViewCategories):
                    _categoryService.ViewCategories();
                    PressAnyKeyToContinue();
                    break;
                case nameof(CategoryMenuOptions.GoBack):
                    goBack = true;
                    break;
            }
        }
    }

    private static string GetOption(IEnumerable<string> choices)
    {
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .AddChoices(choices)
        );

        return WhitespaceRegex().Replace(option, string.Empty);
    }

    private static void PressAnyKeyToContinue()
    {
        Console.Write("\nPress any key to continue...");
        Console.ReadKey();
    }

    [GeneratedRegex("\\s+")]
    private static partial Regex WhitespaceRegex();
}