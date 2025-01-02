using PhoneBook.Bina28;
using Spectre.Console;

var isAppRunning = true;

while (isAppRunning)
{
	Console.Clear();
	var options = AnsiConsole.Prompt(
		new SelectionPrompt<MenuOptions>()
			.Title("Select an option")
			.AddChoices(
			MenuOptions.AddData,
			MenuOptions.RemoveData,
			MenuOptions.ShowAllData,
			MenuOptions.ShowData,
			MenuOptions.UpdateData,
			MenuOptions.Exit)
	);

	switch (options)
	{
		case MenuOptions.AddData:
			PhoneBookService.AddData();
			break;
		case MenuOptions.RemoveData:
			PhoneBookService.DeleteContact();
			break;
		case MenuOptions.ShowAllData:
			PhoneBookService.GetContacts();
			break;
		case MenuOptions.ShowData:
			PhoneBookService.GetContact();
			break;
		case MenuOptions.UpdateData:
			PhoneBookService.UpdateContact();
			break;
		case MenuOptions.Exit:
			AnsiConsole.MarkupLine("[green]Exiting the application. Goodbye![/]");
			isAppRunning = false;
			break;

	}
}
enum MenuOptions
{
	AddData,
	RemoveData,
	ShowAllData,
	ShowData,
	UpdateData,
	Exit
}

