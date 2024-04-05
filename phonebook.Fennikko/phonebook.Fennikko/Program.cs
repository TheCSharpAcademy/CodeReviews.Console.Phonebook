using System.Text.RegularExpressions;
using phonebook.Fennikko;
using PhoneNumbers;

var context = new ContactContext();
var verifyDatabase = context.Database.CanConnect();

if (verifyDatabase  == false) context.Database.EnsureCreated();

UserInterface.MainMenu();