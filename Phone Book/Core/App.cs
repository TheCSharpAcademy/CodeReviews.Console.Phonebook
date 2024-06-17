using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

public class App
{
    private UserInput _userInput;

    public App()
    {
        _userInput = new UserInput();
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
                    break;
                case MainMenuOptions.Update:
                    break;
                case MainMenuOptions.Delete:
                    break;
                case MainMenuOptions.View:
                    break;
            }
            Console.ReadKey();
        }

    }
}
