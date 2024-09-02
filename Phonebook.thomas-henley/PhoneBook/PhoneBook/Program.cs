using PhoneBook;

var cxString = "Server=(localdb)\\MSSQLLocalDB;Database=HenleyPhonebookDB;Integrated Security=true";

var db = new PhonebookDbContext(cxString);

db.Database.EnsureCreated();

db.Contacts.Add(
    new Contact()
    {
        Name = "Thomas Henley",
        Email = "thomas.e.henley@gmail.com",
        Phone = "0123456789",
    });

db.SaveChanges();

db.Database.EnsureDeleted();

db.Dispose();