using Phonebook.K_MYR;

var contactsService = new ContactsService();
var contactsController = new ContactsController(contactsService);

var categoriesService = new CategoriesService();
var categoriesController = new CategoriesController(categoriesService);

var ui = new UserInterface(contactsController, categoriesController);

ui.ShowMainMenu();
