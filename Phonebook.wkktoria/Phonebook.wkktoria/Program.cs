using Phonebook.wkktoria;

var dbContext = new AppDbContext();

dbContext.Database.EnsureDeleted();
dbContext.Database.EnsureCreated();

var ui = new UserInterface();
ui.Run();