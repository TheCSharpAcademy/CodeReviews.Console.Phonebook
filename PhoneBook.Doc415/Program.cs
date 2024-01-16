
namespace PhoneBook.Doc415;

internal class Program
{
    static void Main(string[] args)
    {
        CountrySelection.InitDefaultCountry();
        var userInterface = new UserInterface();
        userInterface.MainMenu();
    }
}

