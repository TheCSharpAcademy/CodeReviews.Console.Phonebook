using Phonebook;
using Spectre.Console;

bool appAlive = true;
while (appAlive)
{
	var option = AnsiConsole.Prompt(new SelectionPrompt<MenuOptions>()
		.Title("What do you want to do ?")
		.AddChoices(
			MenuOptions.AddContact,
			MenuOptions.ShowContact,
			MenuOptions.ShowAllContacts,
			MenuOptions.UpdateContact,
			MenuOptions.DeleteContact,
			MenuOptions.ExitApplication));

	switch(option)
	{
		case MenuOptions.AddContact:
			PhonebookService.InsertContact();
			break;
		case MenuOptions.ShowContact:
			PhonebookService.GetContact();
			break;
		case MenuOptions.ShowAllContacts:
			PhonebookService.GetContacts();
			break;
		case MenuOptions.UpdateContact: break;
		case MenuOptions.DeleteContact: break;
		case MenuOptions.ExitApplication:
			appAlive = false;
			break;
	}
}
enum MenuOptions
{
	AddContact,
	ShowContact,
	ShowAllContacts,
	UpdateContact,
	DeleteContact,
	ExitApplication
}
