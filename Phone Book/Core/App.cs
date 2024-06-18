using Microsoft.EntityFrameworkCore;

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
            switch (option)
            {
                case MainMenuOptions.Add:
                    var contact = _userInput.Add();
                    _contactController.Add(contact);
                    break;
                case MainMenuOptions.Update:
                    break;
                case MainMenuOptions.Delete:
                    break;
                case MainMenuOptions.View:
                    break;
            }
        }

    }
}
