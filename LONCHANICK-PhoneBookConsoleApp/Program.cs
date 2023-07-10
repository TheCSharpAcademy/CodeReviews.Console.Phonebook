using LONCHANICK_PhoneBookConsoleApp;
using Spectre.Console;


bool isAppRunning = true;
while (isAppRunning)
{
	Console.Clear();
	var options = AnsiConsole.Prompt(
	new SelectionPrompt<menuOptions>()
	.Title("What would you like to do?")
	.AddChoices(
		menuOptions.AddContact,
		menuOptions.DeleteContact,
		menuOptions.UpdateContact,
		menuOptions.ViewAllContacts,
		menuOptions.ViewContact,
		menuOptions.Quit));

	Console.Clear();
	switch (options)
	{
		case menuOptions.AddContact:
			ContactService.AddContact();
			break;
		case menuOptions.DeleteContact:
			ContactService.RemoveContact();
			break;
		case menuOptions.UpdateContact:
			ContactService.UpdateContact();
			break;
		case menuOptions.ViewAllContacts:
			ContactService.ViewAllContacts();
			break;
		case menuOptions.ViewContact:
			ContactService.ViewContact();
			break;
		case menuOptions.Quit:
			return;
			//break;
	}
}

enum menuOptions
{
	AddContact,
	DeleteContact,
	UpdateContact,
	ViewAllContacts,
	ViewContact,
	Quit
}