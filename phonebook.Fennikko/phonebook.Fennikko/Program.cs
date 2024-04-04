using phonebook.Fennikko;

var context = new ContactContext();
var verifyDatabase = context.Database.CanConnect();

if (verifyDatabase  == false) context.Database.EnsureCreated();
