using Phonebook.MartinL_no;

ContactController.AddContact("John Doe", "123456");
var contacts = ContactController.GetContacts();
//var contact = ContactController.GetContactById(1);
//ContactController.DeleteContact(1);

var contact = contacts[0];
contact.PhoneNumber = "98765";
ContactController.UpdateContact(contacts[0]);

Console.WriteLine();