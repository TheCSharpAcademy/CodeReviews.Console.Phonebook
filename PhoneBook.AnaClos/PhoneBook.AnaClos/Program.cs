using Microsoft.Extensions.Configuration;
using PhoneBook.AnaClos.Controllers;
using System.Net;

List<string> mainOptions = new List<string> { "Manage Categories", "Manage Contacts", "Exit Program" };
List<string> menuOptions = new List<string> { "Add", "Delete", "Update", "View", "View All", "Exit Menu" };

ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

var builder = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json");

var configuration = builder.Build();
var connectionString = configuration.GetConnectionString("PhoneBookDatabase");

var consoleController = new ConsoleController();
var databaseController = new DataBaseController(connectionString);

var categoriesController = new CategoriesController(consoleController, databaseController);
var contactsController = new ContactsController(consoleController, databaseController, categoriesController);

string option;
do
{
    option = consoleController.Menu("What would you like to do?", "blue", mainOptions);
    switch (option)
    {
        case "Manage Categories":
            Menu(option, categoriesController);
            break;
        case "Manage Contacts":
            Menu(option, contactsController);
            break;
    }

} while (option != "Exit Program");

string Menu(string mainOption, IController controller)
{
    string option;
    do
    {
        option = consoleController.Menu(mainOption, "blue", menuOptions);
        switch (option)
        {
            case "Add":
                controller.Add();
                break;
            case "Delete":
                controller.Delete();
                break;
            case "Update":
                controller.Update();
                break;
            case "View":
                controller.View();
                break;
            case "View All":
                controller.ViewAll();
                break;
        }
    } while (option != "Exit Menu");

    return option;
}
