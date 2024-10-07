using Phone_Book.Lawang;
using Phone_Book.Lawang.Controller;

using var context = new PhoneBookContext();
var contactController = new ContactController(context);
var categoryController = new CategoryController(context);

var application = new Application(contactController, categoryController);
application.Build();
