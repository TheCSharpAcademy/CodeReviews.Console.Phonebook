using Spectre.Console;
using TwilightSaw.Phonebook.Controller;
using TwilightSaw.Phonebook.Helpers;
using TwilightSaw.Phonebook.Model;

namespace TwilightSaw.Phonebook.View
{
    internal class MainMenu(
        ContactController contactController,
        EmailController emailController,
        CategoryController categoryController)
    {
        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new Rule("[olive]Welcome to the Phonebook![/]"));
                AnsiConsole.Write(new Rule("[aqua]Categories[/]"));
                var isDeleted = false;
                var categories = categoryController.Read();
                var chosenCategory = UserInput.CreateCategoryChoosingList(categories, "Exit");
                if (chosenCategory.Name == "Exit") return;
                new CategoryMenu(contactController, emailController, categoryController).Menu(isDeleted, chosenCategory);
            }
        }
    }
}