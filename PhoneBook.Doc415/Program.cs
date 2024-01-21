
namespace PhoneBook.Doc415;

internal class Program
{
    static void Main(string[] args)
    {
        CountrySelection.InitDefaultCountry();
        SetUpEmail.SetUp();
        SMShandler.Setup();
        var userInterface = new UserInterface();
        userInterface.MainMenu();
    }
}

