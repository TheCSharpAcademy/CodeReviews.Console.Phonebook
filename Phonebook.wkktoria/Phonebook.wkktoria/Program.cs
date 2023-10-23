using Phonebook.wkktoria;

var dbContext = new AppDbContext();

dbContext.Database.EnsureDeleted();
dbContext.Database.EnsureCreated();

// var contacts = new ContactService().GetContacts().Select(c => new ContactDto
// {
//     Name = c.Name,
//     Email = c.Email,
//     PhoneNumber = c.PhoneNumber
// }).ToList();
//
// ContactView.ShowContactsTable(contacts);
// ContactView.ShowContactDetails(contacts.Find(c => c.Name == "John"));

// var contactController = new ContactController();
// contactController.ViewContactDetails();

// Console.WriteLine(MainMenuOptions.ManageContacts.ToDescription());

var ui = new UserInterface();
ui.Run();