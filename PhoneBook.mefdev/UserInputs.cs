using PhoneBook.mefdev;
using PhoneBook.mefdev.Service;
using PhoneBook.mefdev.Controllers;

var userInterface = IntializeHelperClasses();
userInterface.MainMenu();


UserInterface IntializeHelperClasses()
{
    var contactService = new ContactService();
    var categoryService = new CategoryService();
    var contactController = new ContactController(contactService, categoryService);
    var categoryController = new CategoryController(categoryService);
    var notificationController = new NotificationController(contactService);
    var userInterface = new UserInterface(contactController, categoryController, notificationController);
    return userInterface;
}