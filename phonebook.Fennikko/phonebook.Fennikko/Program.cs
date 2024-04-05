using phonebook.Fennikko;

var context = new ContactContext();
if (!context.Database.CanConnect()) context.Database.EnsureCreated();

UserInterface.MainMenu();