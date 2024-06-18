using Spectre.Console;

public class App
{
    private UserInput _userInput;
    private ContactRepository _contactRepository;
    private ContactController _contactController;

    public App()
    {
        _userInput = new UserInput();
        _contactRepository = new ContactRepository();
        _contactController = new ContactController(_contactRepository);
    }

    public void Run()
    {
        while (true)
        {
            var option = _userInput.MainMenu();
            var contacts = _contactController.GetAll();
            Contact contact;

            switch (option)
            {
                case MainMenuOptions.Add:
                    // TODO: Handle same name
                    contact = _userInput.Add();
                    _contactController.Add(contact);
                    break;
                case MainMenuOptions.Update:
                    break;
                case MainMenuOptions.Delete:
                    break;
                case MainMenuOptions.ViewByName:
                    contact = _userInput.PickAContact(contacts);
                    _userInput.DisplayContact(contact);
                    break;
                case MainMenuOptions.ViewAll:
                    _userInput.DisplayContacts(contacts);
                    break;
            }
        }

    }
}
