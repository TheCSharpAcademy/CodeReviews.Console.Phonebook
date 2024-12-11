using Phonebook.Controllers;
using Phonebook.Enums;
using Phonebook.Models;
using Phonebook.Services;
using Phonebook.Utilities;
using Phonebook.Controllers;
using Spectre.Console;

namespace Phonebook.Views;

public class AppView
{
    private readonly PhonebookController _phonebookController = new PhonebookController();
    private readonly EmailService _emailService = new EmailService();
    private readonly FilterView _filterView = new FilterView();
    private readonly UpdateView _updateView = new UpdateView();
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
                    _filterView.Run(_phonebookController);
                    break;
                case PhonebookAction.CreateContact:
                    Contact contactToCreate = ContactExtensions.GetContact();
                    _phonebookController.CreateContact(contactToCreate);
                    break;
                case PhonebookAction.UpdateContact:
                    _updateView.Run(_phonebookController);
                    break;
                case PhonebookAction.DeleteContact:
                    _phonebookController.DeleteContact();
                    break;
                case PhonebookAction.SendEmail:
                    _emailService.SendEmail();
                    break;
                case PhonebookAction.Exit:
                    endApp = true;
                    break;
            }
        }
    }
}