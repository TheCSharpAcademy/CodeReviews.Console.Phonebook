using Microsoft.Extensions.Configuration;
using PhoneBook.AnaClos.Controllers;
using System;
using System.Net;

List<string> mainOptions = new List<string> { "Manage Categories", "Manage Contacts", "Exit Program"};
//List<string> categoryOptions = new List<string> { "Add  Category", "Delete Category", "Update Category", "View Category", "View All Categories", "Exit Menu" };
//List<string> contactOptions = new List<string> { "Add  Contact", "Delete Contact", "Update Contact", "View Contact", "View All Contacts", "Exit Menu" };
List<string> menuOptions = new List<string> { "Add", "Delete", "Update", "View", "View All", "Exit Menu" };

ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

var builder = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json");

var configuration = builder.Build();
var connectionString = configuration.GetConnectionString("PhoneBookDatabase");

var consoleController = new ConsoleController();
var databaseController = new DataBaseController(connectionString);
var contactsController = new ContactsController();
var categoriesController = new CategoriesController(consoleController, databaseController);

//var databaseController = new DataBaseController();

string option;
do
{
    option = consoleController.Menu("What would you like to do?", "blue", mainOptions);
    switch (option)
    {
        case "Manage Categories":
            Menu(option,categoriesController);
            break;
        case "Manage Contacts":
            Menu(option,contactsController);
            break;
    }
    
} while (option != "Exit Program") ;

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


//string CategoryMenu()
//{
//    List<string> categoryOptions = new List<string> { "Add  Category", "Delete Category", "Update Category", "View Category", "View All Categories", "Exit Menu" };
//    string option;
//    do
//    {
//        option = consoleController.Menu("", "blue", mainOptions);
//        switch (option)
//        {
//            case "Add Category":
//                categoriesController.AddCategory();
//                break;
//            case "Delete Category":
//                categoriesController.DeleteCategory();
//                break;
//            case "Update Contact":
//                categoriesController.UpdateCategory();
//                break;
//            case "View Contact":
//                categoriesController.ViewCategory();
//                break;
//            case "View All Contacts":
//                categoriesController.ViewAllCategories();
//                break;
//        }
//    } while (option != "Exit");
   
//    return option;
//}

//string ContactMenu()
//{
//    List<string> contactOptions = new List<string> { "Add  Contact", "Delete Contact", "Update Contact", "View Contact", "View All Contacts", "Exit Menu" };
//    switch (option)
//    {
//        case "Add Contact":
//            contactsController.AddContact();
//            break;
//        case "Delete Contact":
//            contactsController.DeleteContact();
//            break;
//        case "Update Contact":
//            contactsController.UpdateContact();
//            break;
//        case "View Contact":
//            contactsController.ViewContact();
//            break;
//        case "View All Contacts":
//            contactsController.ViewAllContacts();
//            break;
//    }
//    return "";
//}
