using System.Data.Common;

namespace PhoneBookProgram;

public class DataController
{
    private readonly int MainMenuOptionsQuantity = 4;
    // private readonly int PageSize = 10;
    public bool RunMainMenuController;
    public bool RunViewContacts;

    public bool RunContactDetail;

    public DataController()
    {
        RunMainMenuController = true;
        RunViewContacts = false;
        RunContactDetail = false;
    }

    public void MainMenuController()
    {
        UI.WelcomeMessage();

        ConsoleKey pressedKey;
        int selection = 0;
        string? errorMessage = null;
            
        while(RunMainMenuController)
        {
            UI.MainMenu(selection, errorMessage);
            pressedKey = Console.ReadKey().Key;
            switch(pressedKey)
            {
                case(ConsoleKey.UpArrow):
                    selection--;
                    if(selection < 0)
                        selection = 0;
                    break;

                case(ConsoleKey.DownArrow):
                    selection++;
                    if(selection > MainMenuOptionsQuantity - 1)
                        selection = MainMenuOptionsQuantity - 1;
                    break;

                case(ConsoleKey.Backspace):
                case(ConsoleKey.Escape):
                    RunMainMenuController = false;
                    break;

                case(ConsoleKey.Enter):
                    switch(selection)
                    {
                        case(0):
                            RunViewContacts = true;
                            ViewContacts();
                            break;
                        case(1):
                            break;
                        case(2):
                            break;
                        case(3):
                            break;                            
                    }
                    break;
            }
        }

        UI.ExitMessage(errorMessage);
    }

    public void ViewContacts()
    {
        var contacts = new List<Contact>();
        var contactsToUI = new List<ContactDtoWithSelection>();
        
        bool getContacts = true;
        int selection = 0;
        int prevSelection = 0;
        ConsoleKey pressedKey;

        while(RunViewContacts)
        {
            if(getContacts)
            {
                using var dbController = new DBController();
                {
                    contacts = dbController.GetContacts();
                    contactsToUI = 
                        contacts.Select(contact => new ContactDtoWithSelection(contact)).ToList();
                }
                getContacts = false;
                selection = 0;
                prevSelection = 0;
            }

            UI.DisplayContacts(contactsToUI, selection, prevSelection);
            pressedKey = Console.ReadKey().Key;
            switch(pressedKey)
            {
                case(ConsoleKey.UpArrow):
                    prevSelection = selection;
                    selection--;
                    if(selection < 0)
                        selection = 0;
                    break;

                case(ConsoleKey.DownArrow):
                    prevSelection = selection;
                    selection++;
                    if(selection > contactsToUI.Count - 1)
                        selection = contactsToUI.Count - 1;
                    break;

                case(ConsoleKey.Backspace):
                case(ConsoleKey.Escape):
                    RunViewContacts = false;
                    break;
                case(ConsoleKey.D):
                    getContacts = DeleteContact(contacts[selection].ContactId);
                    break;

                case(ConsoleKey.I):
                    getContacts = InsertContact();
                    break;

                case(ConsoleKey.M):
                    getContacts = ModifyContact(contacts[selection].ContactId);
                    break;

                case(ConsoleKey.Enter):
                    RunContactDetail = true;
                    ViewContactDetail(contacts[selection].ContactId);
                    break;
            }
        }
    }

    public static bool DeleteContact(int contactId)
    {
        UI.DisplayConfirmationPromt("contact");
        var selection = Console.ReadLine() ?? "";
        if(selection.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                using var dbController = new DBController();
                {
                    dbController.DeleteContact(contactId);
                }
                return true;
            }
        return false;
    }

    public static bool ModifyContact(int contactId)
    {
        var validContact = true;
        string modifyContactName;
        while(validContact)
        {
            UI.DisplayInsert("modify contact"); //Pending UI
            modifyContactName = Console.ReadLine() ?? "";  //pending validation pending cancel
            if(true)
            {
                using var dbController = new DBController();
                    {
                        dbController.ModifyContact(modifyContactName, contactId);
                    }
                return true;
            }
        }        
        return false;
    }

    public static bool InsertContact()
    {
        var validContact = true;
        string newContactName;
        while(validContact)
        {
            UI.DisplayInsert("contact");
            newContactName = Console.ReadLine() ?? "";  //pending validation pending cancel
            if(true)
            {
                using var dbController = new DBController();
                    {
                        dbController.InsertContact(newContactName);
                    }
                return true;
            }
        }        
        return false;
    }

    public void ViewContactDetail(int contactId)
    {
        var emailsDto = new List<EmailDto>();
        var phonesDto = new List<PhoneNumberDto>();
        bool getContactDetails = true;
        int selection = 0;
        int prevSelection = 0;
        ConsoleKey pressedKey;
        
        while(RunContactDetail)
        {
            if(getContactDetails)
            {
                using var dbController = new DBController();
                {
                    emailsDto = dbController.GetEmails(contactId)
                        .Select(p => new EmailDto(p)).ToList();
                    phonesDto = dbController.GetPhones(contactId)
                        .Select(p => new PhoneNumberDto(p)).ToList();
                }
                getContactDetails = false;
            } 
            UI.DisplayContactData(emailsDto, phonesDto, selection, prevSelection, "test");
            pressedKey = Console.ReadKey().Key;
            switch(pressedKey)
            {
                case(ConsoleKey.UpArrow):
                    prevSelection = selection;
                    selection--;
                    if(selection < 0)
                        selection = 0;
                    break;

                case(ConsoleKey.DownArrow):
                    prevSelection = selection;
                    selection++;
                    if(selection > phonesDto.Count + emailsDto.Count - 1)
                        selection = phonesDto.Count + emailsDto.Count - 1;
                    break;
                case(ConsoleKey.I):
                    break;
                case(ConsoleKey.D):
                    break;
                case(ConsoleKey.M):
                    break;
                case(ConsoleKey.Backspace):
                case(ConsoleKey.Escape):
                    RunContactDetail = false;
                    break;
            }
        }
    }
}