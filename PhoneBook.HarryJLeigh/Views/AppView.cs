using Phonebook.Enums;
using Phonebook.Models;
using Phonebook.Services;
using Phonebook.Utilities;
using Spectre.Console;

namespace Phonebook.Views;

public class UserInterface
{
    internal void Run()
    {
        bool endApp = false;

        while (!endApp)
        {
            Console.Clear();
            var enumPhoneActionValues = Enum.GetValues(typeof(PhonebookAction)).Cast<PhonebookAction>().ToList();
            var selectedPhonebook = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .AddChoices(enumPhoneActionValues.Select(a => a.GetDisplayName())));
            
            var selectedAction = enumPhoneActionValues.FirstOrDefault(a => a.GetDisplayName() == selectedPhonebook);

            switch (selectedAction)
            {
                case PhonebookAction.ViewContacts:
                    FilterView.Run();
                    break;
                case PhonebookAction.CreateContact:
                    Contact contactToCreate = ContactExtensions.GetContact();
                    PhonebookService.CreateContact(contactToCreate);
                    break;
                case PhonebookAction.UpdateContact:
                    UpdateView.Run();
                    break;
                case PhonebookAction.DeleteContact:
                    PhonebookService.DeleteContact();
                    break;
                case PhonebookAction.SendEmail:
                    EmailService.SendEmail();
                    break;
                case PhonebookAction.Exit:
                    endApp = true;
                    break;
            }
        }
    }
}