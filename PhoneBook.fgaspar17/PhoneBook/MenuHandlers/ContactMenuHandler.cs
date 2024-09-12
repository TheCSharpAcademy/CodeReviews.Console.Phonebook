using Spectre.Console;

namespace PhoneBook;

public class ContactMenuHandler
{
    public void Display()
    {
        MenuPresentation.MenuDisplayer<ContactMenuOptions>(() => "[blue]Contact Menu[/]", HandleMenuOptions);
    }

    private bool HandleMenuOptions(ContactMenuOptions option)
    {
        switch (option)
        {
            case ContactMenuOptions.Quit:
                return false;
            case ContactMenuOptions.CreateContact:
                ContactService.CreateContact();
                break;
            case ContactMenuOptions.UpdateContact:
                ContactService.UpdateContact();
                break;
            case ContactMenuOptions.DeleteContact:
                ContactService.DeleteContact();
                break;
            case ContactMenuOptions.ShowContact:
                ContactService.ShowContact();
                break;
            case ContactMenuOptions.ShowContacts:
                ContactService.ShowContacts();
                break;
            case ContactMenuOptions.ManageCategories:
                CategoryMenuHandler categoryMenuHandler = new CategoryMenuHandler();
                categoryMenuHandler.Display();
                break;
            case ContactMenuOptions.GroupContactsByCategories:
                ContactCategoryMenuHandler contactCategoryMenuHandler = new ContactCategoryMenuHandler();
                contactCategoryMenuHandler.Display();
                break;
            case ContactMenuOptions.SendMail:
                SenderService.Send(SenderType.Mail);
                break;
            case ContactMenuOptions.SendSms:
                SenderService.Send(SenderType.Sms);
                break;
            default:
                AnsiConsole.WriteLine($"Unknow option: {option}");
                break;
        }

        return true;
    }
}