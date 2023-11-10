using Microsoft.Data.SqlClient;
using Phonebook.K_MYR;
using Phonebook.K_MYR.Models;

var contactsService = new ContactsService();
var contactsController = new ContactsController(contactsService);

var categoriesService = new CategoriesService();
var categoriesController = new CategoriesController(categoriesService);

var ui = new UserInterface(contactsController, categoriesController);

try
{
    var db = new ContactsContext();
    db.Database.EnsureCreated();
}
catch (SqlException ex)
{
    Helpers.WriteMessageAndWait($"An Error Occured Creating The Database | {ex.Message} | Press Any Key To Continue Nevertheless");
}

ui.ShowMainMenu();
