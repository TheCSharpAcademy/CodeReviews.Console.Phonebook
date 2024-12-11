using Phonebook.Controllers;
using Phonebook.Enums;
using Spectre.Console;

namespace Phonebook.Views;

public  class UpdateView
{
    internal void Run(PhonebookController _phonebookController)
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
                    _phonebookController.UpdateContact(updateName: true);
                    break;
                case UpdateOptions.Email:
                    _phonebookController.UpdateContact(updateEmail: true);
                    break;
                case UpdateOptions.Number:
                    _phonebookController.UpdateContact(updateNumber: true);
                    break;
                case UpdateOptions.Category:
                    _phonebookController.UpdateContact(updateCategory: true);
                    break;
                case UpdateOptions.All:
                    _phonebookController.UpdateContact(
                        updateName: true, 
                        updateEmail: true, 
                        updateNumber: true,
                        updateCategory: true);
                    break;
                case UpdateOptions.Exit:
                    endUpdateView = true;
                    break;
            }
        }
    }
}