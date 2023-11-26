using Phonebook.StanimalTheMan;

var context = new ContactsContext();
context.Database.EnsureDeleted();
context.Database.EnsureCreated();

UserInterface.MainMenu();
// codacy wouldn't run, maybe make another commit after opening folder containing everyone's solutions...