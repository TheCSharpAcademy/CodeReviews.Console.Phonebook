using Phonebook.Enums;
using Phonebook.Services;
using Phonebook.Utilities;
using Spectre.Console;

namespace Phonebook.Views;

public static class FilterView
{
    internal static void Run()
    {
        bool endFilterView = false;
        while (!endFilterView)
        {
            Console.Clear();
            var filterChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<FilterOptions>()
                        .Title("What would you like to filter by:")
                        .AddChoices(Enum.GetValues<FilterOptions>()));
           
            switch (filterChoice)
            {
                case FilterOptions.All:
                    PhonebookService.ViewContacts();
                    Util.AskUserToContinue();
                    break;
                case FilterOptions.Friends:
                    PhonebookService.ViewContactsByFilter("Friends");
                    break;
                case FilterOptions.Family:
                    PhonebookService.ViewContactsByFilter("Family");
                    break;
                case FilterOptions.Work:
                    PhonebookService.ViewContactsByFilter("Work");
                    break;
                case FilterOptions.Exit:
                    endFilterView = true;
                    break;
            }
        }
    }
}