namespace PhoneBookProgram;

public class DataController
{
    private readonly int MainMenuOptionsQuantity = 4;
    // private readonly int PageSize = 10;
    public bool RunMainMenuController;
    public bool RunViewContacts;

    public DataController()
    {
        RunMainMenuController = true;
        RunViewContacts = false;
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
        using var dbController = new DBController();
        var contacts = dbController.GetContacts();
        var contactsToUI = 
            contacts.Select(contact => new ContactDTOWithSelection(contact)).ToList();
        
        int selection = 0;
        int prevSelection = 0;
        ConsoleKey pressedKey;

        while(RunViewContacts)
        {
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
                    if(selection > contacts.Count - 1)
                        selection = contacts.Count - 1;
                    break;

                case(ConsoleKey.Backspace):
                case(ConsoleKey.Escape):
                    RunViewContacts = false;
                    break;

                case(ConsoleKey.Enter):
                    var emailData = contacts[selection].Emails.Select( 
                        email => new EmailDTO(email)).ToList();
                    var phoneData = contacts[selection].PhoneNumbers.Select( 
                        phone => new PhoneNumberDTO(phone)).ToList();
                    UI.DisplayContactData(emailData, phoneData, 2, contacts[selection].ContactName);
                    Console.ReadKey();
                    break;
            }
        }
    }
}