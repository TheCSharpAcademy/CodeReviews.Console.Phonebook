using PhoneBook.Interfaces.Handlers;
using PhoneBook.Interfaces.Handlers.ContactHandlers;
using PhoneBook.Interfaces.Services;
using PhoneBook.Services;

namespace PhoneBook.Menu.Commands.MainMenuCommands;

internal sealed class ViewAllContactsCommand : DisplayingContactsCommand
{
    private readonly IContactSelector _contactSelector;
    private readonly IEmailSender _emailSender;
    
    public ViewAllContactsCommand(
        IContactSelector contactSelector,
        IContactTableConstructor contactTableConstructor,
        IEmailSender emailSender
        ) : base(contactTableConstructor)
    {
        _contactSelector = contactSelector;
        _emailSender = emailSender;
    }
    
    public override void Execute()
    {
        _contactSelector.SelectContact(out var contact, out var message);

        if (ContactIsNull(contact, message))
        {
            return;
        }
        
        DisplayContact(contact);

        bool sendAnEmail = PromptService.ConfirmAction(SendAnEmail);
        if (sendAnEmail)
        {
            _emailSender.SendEmail(contact);
        }

        HelperService.PressAnyKey();
    }
}