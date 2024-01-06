using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PhoneBookProgram;

public class DataController
{
    private readonly int MainMenuOptionsQuantity = 3;
    private readonly int PageSize = 10;
    public bool RunMainMenuController;

    public DataController()
    {
        RunMainMenuController = true;
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
                    if(selection > MainMenuOptionsQuantity)
                        selection = MainMenuOptionsQuantity;
                    break;

                case(ConsoleKey.Backspace):
                case(ConsoleKey.Escape):
                    RunMainMenuController = false;
                    break;

                case(ConsoleKey.Enter):
                    switch(selection)
                    {
                        case(0):
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

    public static void ViewContacts()
    {
        using var dbController = new DBController();
        var contacts = dbController.GetContacts();
        List<ContactDTO>? contactsToUI = contacts.Select(contact => new ContactDTO(contact)).ToList();
        UI.DisplayContacts(contactsToUI);
        Console.ReadKey();
    }
}