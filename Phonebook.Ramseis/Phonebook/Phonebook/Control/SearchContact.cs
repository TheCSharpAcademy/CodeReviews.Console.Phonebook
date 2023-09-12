using System.Diagnostics.Metrics;

namespace Phonebook.Control;

internal class SearchContact
{
    private readonly static List<ConsoleKey> keys = new List<ConsoleKey> {
        ConsoleKey.D0, ConsoleKey.D1,ConsoleKey.D2,ConsoleKey.D3,ConsoleKey.D4,ConsoleKey.D5,ConsoleKey.D6,ConsoleKey.D7,ConsoleKey.D8,ConsoleKey.D9,
        ConsoleKey.NumPad0,ConsoleKey.NumPad1,ConsoleKey.NumPad2,ConsoleKey.NumPad3,ConsoleKey.NumPad4,ConsoleKey.NumPad5,
        ConsoleKey.NumPad6,ConsoleKey.NumPad7,ConsoleKey.NumPad8,ConsoleKey.NumPad9
    };
    public static void Menu()
    {
        Menu menu = new Menu
        {
            Titles = new List<string> { "Search Contacts" },
            Message = "Type name above or escape to return to main menu.\n  Select from search results with number below..."
        };

        string query = "";
        ConsoleKeyInfo input;
        bool run = true;
        menu.Draw();

        while (run)
        {
            Console.Clear();
            menu.Draw();
            Console.SetCursorPosition(2, menu.InputRow);
            Console.Write(query);
            Console.SetCursorPosition(0, menu.InputRow + 3);
            List<Contact> contacts = new();
            if (query.Length > 0)
            {
                contacts = Controller.GetContacts().Where(x => x.Name.ToUpper().Contains(query.ToUpper())).ToList();
                int i = 1;
                List<List<Object>> formatted = new();
                foreach (Contact contact in contacts)
                {
                    List<Object> list = new();
                    list.Add(i);
                    i++;
                    list.Add(contact.Name);
                    list.Add(contact.Phone);
                    list.Add(contact.Group);
                    formatted.Add(list);
                    if (i > 7) { break; }
                }
                ConsoleTable.PrintSearch(formatted);
            }
            Console.SetCursorPosition(2 + query.Length, menu.InputRow);

            input = Console.ReadKey(true);

            if ((input.Modifiers & ConsoleModifiers.Alt) == ConsoleModifiers.Alt) { continue; }
            if ((input.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control) { continue; }
            if (input.KeyChar == '\u0000') { continue; }
            if (input.Key == ConsoleKey.Tab) { continue; }
            if (input.Key == ConsoleKey.Escape) { return; }
            if (keys.Contains(input.Key))
            {
                int selected = keys.IndexOf(input.Key);
                selected = selected > 9 ? selected - 10 : selected;
                if (selected > 0 & selected <= contacts.Count)
                {
                    ViewContact.Menu(contacts[selected - 1]);
                    break;
                } else { continue; }
            }
            if (input.Key == ConsoleKey.Backspace & query.Length > 0)
            {
                query = query.Remove(query.Length - 1);
                continue;
            }
            query += input.KeyChar;
        }
    }
}
