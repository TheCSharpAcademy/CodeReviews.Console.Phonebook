using Phonebook.Enums;
using Phonebook.Services;
using Spectre.Console;

namespace Phonebook.Views;

public static class UpdateView
{
    internal static void Run()
    {
        bool endUpdateView = false;
        while (!endUpdateView)
        {
            Console.Clear();
            var updateChoice = AnsiConsole.Prompt(
                new SelectionPrompt<UpdateOptions>()
                    .Title("What would you like to update?")
                    .AddChoices(Enum.GetValues<UpdateOptions>()));

            switch (updateChoice)
            {
                case UpdateOptions.Name:
                    PhonebookService.UpdateContact(updateName: true);
                    break;
                case UpdateOptions.Email:
                    PhonebookService.UpdateContact(updateEmail: true);
                    break;
                case UpdateOptions.Number:
                    PhonebookService.UpdateContact(updateNumber: true);
                    break;
                case UpdateOptions.Category:
                    PhonebookService.UpdateContact(updateCategory: true);
                    break;
                case UpdateOptions.All:
                    PhonebookService.UpdateContact(updateName: true, updateEmail: true, updateNumber: true);
                    break;
                case UpdateOptions.Exit:
                    endUpdateView = true;
                    break;
            }
        }
    }
}