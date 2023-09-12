using Phonebook;
using Phonebook.Control;

Controller.Init();

Console.Title = "Contact Book";

bool run = true;

Menu menu = new Menu
{
    Titles = new List<string>
    {
        "Electronic-Rolodex"
    },
    Options = new List<string>
    {
        " 1. Recent Contacts",
        " 2. List Contacts",
        " 3. Search Contacts",
        " 4. New Contact",
        "",
        " 9. Load test contacts",
        " 0. Exit"
    },
    Message = "Select from the numbered options above..."
};

while (run)
{
    menu.Draw();
    ConsoleKey input = Console.ReadKey().Key;
    if (input == ConsoleKey.D1 | input == ConsoleKey.NumPad1)
    {
        ListContact.Recent();
    }
    else if (input == ConsoleKey.D2 | input == ConsoleKey.NumPad2)
    {
        ListContact.Menu();
    }
    else if (input == ConsoleKey.D3 | input == ConsoleKey.NumPad3)
    {
        SearchContact.Menu();
    }
    else if (input == ConsoleKey.D4 | input == ConsoleKey.NumPad4)
    {
        NewContact.Menu();
    }
    else if (input == ConsoleKey.D9 | input == ConsoleKey.NumPad9)
    {
        DefaultContact.Load();
    }
    else if (input == ConsoleKey.D0 | input == ConsoleKey.NumPad0)
    {
        run = false;
    }
}

Environment.Exit(0); 