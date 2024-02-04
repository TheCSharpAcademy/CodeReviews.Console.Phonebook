﻿using Phonebook.frockett.DataLayer;
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

        
        var inputValidator = new InputValidator();
        var userInput = new HandleUserInput(inputValidator);
        var tableEngine = new TableEngine();
        var menuHandler = new MenuHandler(phonebookService, tableEngine, userInput);

        menuHandler.ShowMainMenu();
    }
}
