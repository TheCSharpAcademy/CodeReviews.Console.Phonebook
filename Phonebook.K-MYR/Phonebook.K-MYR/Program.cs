using Phonebook.K_MYR;

var service = new ContactsService();
var controller = new ContactsController(service);
var ui = new UserInterface(controller);

ui.ShowContactsMenu();
