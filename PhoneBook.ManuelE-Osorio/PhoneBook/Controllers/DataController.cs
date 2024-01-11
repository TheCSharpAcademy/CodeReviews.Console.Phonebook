using System.Data.Common;

namespace PhoneBookProgram;

public class DataController
{
    private readonly int MainMenuOptionsQuantity = 4;
    // private readonly int PageSize = 10;
    public bool RunViewContacts;

    public bool RunContactDetail;
    public DBController DBInstance;

    public DataController()
    {
        RunViewContacts = true;
        RunContactDetail = false;
        DBInstance = new();
    }

    public void MainMenuController()
    {
        UI.WelcomeMessage();

        var contacts = new List<Contact>();
        var contactsToUI = new List<ContactDtoWithSelection>();    
        bool getContacts = true;
        int selection = 0;
        int prevSelection = 0;
        ConsoleKey pressedKey;
        string? errorMessage = null;

        while(RunViewContacts)
        {
            if(getContacts)
            {
                contacts = DBInstance.GetContacts();
                contactsToUI = contacts
                    .Select(contact => new ContactDtoWithSelection(contact)).ToList();
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
        UI.ExitMessage(errorMessage);
    }

    public bool DeleteContact(int contactId)
    {
        UI.DisplayConfirmationPromt("contact");
        var selection = Console.ReadLine() ?? "";
        if(selection.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            DBInstance.Delete(contactId);
            return true;
        }
        return false;
    }

    public bool ModifyContact(int contactId)
    {
        var validContact = true;
        string modifyContactName;
        while(validContact)
        {
            UI.DisplayInsert("modify contact"); //Pending UI
            modifyContactName = Console.ReadLine() ?? "";  //pending validation pending cancel
            if(true)
            {
                DBInstance.Modify(modifyContactName, contactId);
                return true;
            }
        }        
        return false;
    }

    public bool InsertContact()
    {
        var validContact = true;
        string newContactName;
        while(validContact)
        {
            UI.DisplayInsert("contact");
            newContactName = Console.ReadLine() ?? "";  //pending validation pending cancel
            if(true)
            {
                DBInstance.Insert(newContactName);
                return true;
            }
        }        
        return false;
    }

    public void ViewContactDetail(int contactId)
    {
        var contact = new Contact();
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
                
                contact = DBInstance.GetEmails(contactId);
                emailsDto = contact.Emails
                    .Select(p => new EmailDto(p)).ToList();
                phonesDto = contact.PhoneNumbers
                    .Select(p => new PhoneNumberDto(p)).ToList();
                getContactDetails = false;
                selection = 0;
                prevSelection = 0;
            } 

            UI.DisplayContactData(phonesDto, emailsDto, selection, prevSelection, "test");
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
                    if(selection < phonesDto.Count)
                        getContactDetails = Insert("phone number", new PhoneNumber{ContactId = contactId});
                    else
                        getContactDetails = Insert("email", new Email{ ContactId = contactId});
                    break;
                case(ConsoleKey.D):
                    if(selection < phonesDto.Count)
                        getContactDetails = Delete("phone number", contact.PhoneNumbers[selection]);
                    else
                        getContactDetails = Delete("email", contact.Emails[selection - phonesDto.Count]);
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

    public bool Delete(string objectType, Email objectToDelete)
    {
        UI.DisplayConfirmationPromt(objectType);
        var selection = Console.ReadLine() ?? "";
        if(selection.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                DBInstance.Delete(objectToDelete);
                return true;
            }
        return false;
    }

    public bool Delete(string objectType, PhoneNumber objectToDelete)
    {
        UI.DisplayConfirmationPromt(objectType);
        var selection = Console.ReadLine() ?? "";
        if(selection.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                
                DBInstance.Delete(objectToDelete);
                
                return true;
            }
        return false;
    }
    public bool Insert(string objectType,Email emailToInsert) // improvemethod
    {
        UI.DisplayInsert(objectType);

        var validContact = true;
        string data;
        string data2;
        while(validContact)
        {
            UI.DisplayInsert(objectType);
            data = Console.ReadLine() ?? "";  //pending validation pending cancel, pending divide string
            data2 = Console.ReadLine() ?? ""; 
            if(true)
            {
                emailToInsert.LocalName = data;
                emailToInsert.DomainName = data2;
                DBInstance.Insert(emailToInsert);
                return true;
            }
        }        
        return false;
    }

    public bool Insert(string objectType, PhoneNumber phoneToInsert) // improvemethod
    {
        UI.DisplayInsert(objectType);

        var validContact = true;
        string data;
        string data2;
        while(validContact)
        {
            UI.DisplayInsert(objectType);
            data = Console.ReadLine() ?? "";  //pending validation pending cancel, pending divide string
            data2 = Console.ReadLine() ?? ""; 
            if(true)
            {
                phoneToInsert.CountryCode = data;
                phoneToInsert.LocalNumber = data2;
                DBInstance.Insert(phoneToInsert);
                return true;
            }
        }        
        return false;
    }
}