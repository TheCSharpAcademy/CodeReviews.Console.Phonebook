using Phonebook.yemiOdetola;

var repo = new ContactsRepository();
await repo.TestConnection();

var menu = new Menu();
await menu.Show();
