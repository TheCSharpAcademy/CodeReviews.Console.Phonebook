using PhoneBook.Data;
using PhoneBook.UserInterfaceControllers;

MainMenu mainMenu = new();
while (true)
{
    Console.Clear();
    var choice = mainMenu.ShowMainMenu();
    if (choice == 3)

    { break; }

    using var db = new PhoneBookContext();
    switch (choice)
    {
        case 1:
            ViewContactsMenu viewContactsMenu = new(db);
            await viewContactsMenu.ListAllContactsAsync();
            break;
        case 2:
            AddContactMenu addContactMenu = new(db);
            await addContactMenu.PromptNewContact();
            break;
    }
}