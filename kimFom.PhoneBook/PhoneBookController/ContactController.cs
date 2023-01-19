using PhoneBookConsole.Input;
using PhoneBookConsole.PhoneBookService;

namespace PhoneBookConsole.PhoneBookController;

public class ContactController : IContactController
{
    private readonly IContactService _contactService = new ContactService();
    private readonly IUserInput _input = new UserInput();

    public void LaunchProgram()
    {
        Menus.DisplayMainMenu();
        var choice = _input.GetInput();
        while (choice != "0")
        {
            switch (choice)
            {
                case "1":
                    _contactService.ViewAllContacts();
                    break;
                case "2":
                    _contactService.AddNewContact();
                    break;
                case "3":
                    _contactService.EditContact();
                    break;
                case "4":
                    _contactService.DeleteContact();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    break;
            }

            Menus.DisplayMainMenu();
            choice = _input.GetInput();
        }
    }
}