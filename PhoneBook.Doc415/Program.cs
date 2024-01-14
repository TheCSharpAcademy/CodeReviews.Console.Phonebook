using PhoneBook.Doc415.Context;
using PhoneBook.Doc415.Models;
namespace PhoneBook.Doc415;

internal class Program
{
    static void Main(string[] args)
    {
        var db = new PhoneBookContext();
        CountrySelection.InitDefaultCountry();
        var userInterface = new UserInterface();
        userInterface.MainMenu();
    }
}

