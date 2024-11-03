using Console.Phonebook.App.Data;
using Console.Phonebook.App.UI;

namespace Console.Phonebook.App;

internal class Program
{
    static void Main(string[] args)
    {
        ApplicationDbContext context = new ApplicationDbContext();

        context.Database.EnsureCreated();
        MainMenuUI mainMenu = new MainMenuUI();
        mainMenu.MainMenuSelection();
    }
}
