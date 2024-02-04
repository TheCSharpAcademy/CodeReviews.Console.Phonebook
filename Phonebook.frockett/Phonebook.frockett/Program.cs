using Phonebook.frockett.DataLayer;
using Phonebook.frockett.Service_Layer;
using Phonebook.frockett.UI;

namespace Phonebook.frockett;

internal class Program
{
    static void Main(string[] args)
    {
        var dbContext = new PhoneBookContext();
        var phonebookRepository = new PhoneBookRepository(dbContext);

        var phonebookService = new PhonebookService(phonebookRepository);

        var userInput = new HandleUserInput();
        var inputValidator = new InputValidator();
        var tableEngine = new TableEngine();
        var menuHandler = new MenuHandler(phonebookService, inputValidator, tableEngine, userInput);

        menuHandler.ShowMainMenu();
    }
}
