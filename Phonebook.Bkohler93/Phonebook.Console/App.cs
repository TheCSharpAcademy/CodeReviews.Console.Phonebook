using Phonebook.Console.Config;
using Phonebook.Console.Controllers;
using Phonebook.Console.Data;
using Phonebook.Console.UserInterface;

namespace Phonebook.Console;

public class App {
    private AppDbContext db;
    private MainController controller;
    private AppConfig config;

    public App() {
        db = new();
        config = new();
        controller = new(db, config);
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
        