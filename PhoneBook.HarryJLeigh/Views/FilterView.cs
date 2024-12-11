using Phonebook.Controllers;
using Phonebook.Enums;
using Phonebook.Utilities;
using Spectre.Console;

namespace Phonebook.Views;

public class FilterView
{
    internal void Run(PhonebookController _phonebookController)
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
                    _phonebookController.ViewContacts();
                    Util.AskUserToContinue();
                    break;
                case FilterOptions.Friends:
                    _phonebookController.ViewContactsByFilter("Friends");
                    break;
                case FilterOptions.Family:
                    _phonebookController.ViewContactsByFilter("Family");
                    break;
                case FilterOptions.Work:
                    _phonebookController.ViewContactsByFilter("Work");
                    break;
                case FilterOptions.Exit:
                    endFilterView = true;
                    break;
            }
        }
    }
}