using Phonebook.StanimalTheMan;

var context = new ContactsContext();
context.Database.EnsureDeleted();
context.Database.EnsureCreated();

UserInterface.MainMenu();