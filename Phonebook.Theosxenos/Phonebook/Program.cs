using Phonebook.Controllers;
using Phonebook.Data;
using Phonebook.Views;
using Spectre.Console;

var controller = new PhoneBookController();
var view = new BaseView();
var runApp = true;
var mainMenu = new Dictionary<string, Action>
{
    ["Add Contact"] = controller.AddContact,
    ["Update Contact"] = controller.UpdateContact,
    ["List Contacts"] = controller.ListContacts,
    ["Delete Contact"] = controller.DeleteContact,
    ["Exit"] = () => runApp = false
};

using (PhoneBookContext db = new())
{
    db.Database.EnsureCreated();
}

AnsiConsole.Write(new FigletText("Phone Book").Color(Color.Yellow2));
while (runApp)
{
    var choice = view.ShowMenu(mainMenu.Keys.ToArray());
    mainMenu[choice]();
}
