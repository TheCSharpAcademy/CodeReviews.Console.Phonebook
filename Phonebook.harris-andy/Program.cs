using Phonebook;

internal class Program
{
    private static void Main(string[] args)
    {
        UserInput userInput = new UserInput();
        DisplayData displayData = new DisplayData();
        ContactsDataManager dataManager = new ContactsDataManager();
        CategoryDataManager categoryDataManager = new CategoryDataManager();
        PhonebookController phonebookController = new PhonebookController(displayData, userInput, dataManager, categoryDataManager);

        phonebookController.ShowMainMenu();
    }
}
