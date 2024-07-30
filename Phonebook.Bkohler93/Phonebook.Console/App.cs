using Phonebook.Console.Controllers;
using Phonebook.Console.Data;
using Phonebook.Console.UserInterface;

namespace Phonebook.Console;

public class App {
    private AppDbContext db;
    private MainController controller;

    public App() {
        db = new();
        controller = new(db);
    }

    public void Run() {
        while (true) {
            var choice = UI.MainMenu();

            if (choice == MainController.Exit) {
                break;
            }
            
            controller.HandleChoice(choice);
        }
    }
}
        