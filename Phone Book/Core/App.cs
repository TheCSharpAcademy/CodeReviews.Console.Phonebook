using Spectre.Console;

public class App
{
    private UserInput _userInput;

    public App()
    {
        _userInput = new UserInput();
    }

    public void Run()
    {
        var option = _userInput.MainMenu();
        switch (option)
        {
            case MainMenuOptions.Add:
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

public class UserInput
{
    

    public MainMenuOptions MainMenu()
    {
        var input = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Please choose an action:")
            .PageSize(10)
            .AddChoices(Enum.GetNames(typeof(MainMenuOptions)).ToList())
            );

        return Enum.Parse<MainMenuOptions>(input);
    }
}
