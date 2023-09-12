using ConsoleTableExt;
using Phonebook.Control;

namespace Phonebook;

internal class ConsoleTable
{
    public static void PrintTable(List<List<object>> data)
    {
        Console.Clear();
        ConsoleTableBuilder
            .From(data)
            .WithTitle("Contacts")
            .WithColumn("Select", "Name", "Phone", "Group")
            .WithTextAlignment(new Dictionary<int, TextAligntment>
            {
                {0, TextAligntment.Left },
                {1, TextAligntment.Center },
                {2, TextAligntment.Right },
            })
            .WithCharMapDefinition(CharMapDefinition.FramePipDefinition)
            .WithCharMapDefinition(
                CharMapDefinition.FramePipDefinition,
                new Dictionary<HeaderCharMapPositions, char> {
                    {HeaderCharMapPositions.TopLeft, '╒' },
                    {HeaderCharMapPositions.TopCenter, '═' },
                    {HeaderCharMapPositions.TopRight, '╕' },
                    {HeaderCharMapPositions.BottomLeft, '╞' },
                    {HeaderCharMapPositions.BottomCenter, '╤' },
                    {HeaderCharMapPositions.BottomRight, '╡' },
                    {HeaderCharMapPositions.BorderTop, '═' },
                    {HeaderCharMapPositions.BorderRight, '│' },
                    {HeaderCharMapPositions.BorderBottom, '═' },
                    {HeaderCharMapPositions.BorderLeft, '│' },
                    {HeaderCharMapPositions.Divider, ' ' },
                })
            .ExportAndWriteLine();
    }
    public static void PrintSearch(List<List<object>> data)
    {
        ConsoleTableBuilder
            .From(data)
            .WithColumn("Select", "Name", "Phone", "Group")
            .WithTextAlignment(new Dictionary<int, TextAligntment>
            {
                {0, TextAligntment.Left },
                {1, TextAligntment.Center },
                {2, TextAligntment.Right },
            })
            .WithCharMapDefinition(CharMapDefinition.FramePipDefinition)
            .WithCharMapDefinition(
                CharMapDefinition.FramePipDefinition,
                new Dictionary<HeaderCharMapPositions, char> {
                    {HeaderCharMapPositions.TopLeft, '╒' },
                    {HeaderCharMapPositions.TopCenter, '═' },
                    {HeaderCharMapPositions.TopRight, '╕' },
                    {HeaderCharMapPositions.BottomLeft, '╞' },
                    {HeaderCharMapPositions.BottomCenter, '╤' },
                    {HeaderCharMapPositions.BottomRight, '╡' },
                    {HeaderCharMapPositions.BorderTop, '═' },
                    {HeaderCharMapPositions.BorderRight, '│' },
                    {HeaderCharMapPositions.BorderBottom, '═' },
                    {HeaderCharMapPositions.BorderLeft, '│' },
                    {HeaderCharMapPositions.Divider, ' ' },
                })
            .ExportAndWriteLine();
    }
}
internal class ListContact
{
    private readonly static List<ConsoleKey> keys = new List<ConsoleKey> {
        ConsoleKey.D0, ConsoleKey.D1,ConsoleKey.D2,ConsoleKey.D3,ConsoleKey.D4,ConsoleKey.D5,ConsoleKey.D6,ConsoleKey.D7,ConsoleKey.D8,ConsoleKey.D9,
        ConsoleKey.NumPad0,ConsoleKey.NumPad1,ConsoleKey.NumPad2,ConsoleKey.NumPad3,ConsoleKey.NumPad4,ConsoleKey.NumPad5,
        ConsoleKey.NumPad6,ConsoleKey.NumPad7,ConsoleKey.NumPad8,ConsoleKey.NumPad9
    };
    public static void Menu()
    {
        List<Contact> contacts = Controller.GetContacts();
        int count = contacts.Count;
        for (int i = 0; i < Math.Ceiling(count/9d); i++)
        {
            List<Contact> subcontacts = new();
            if (count < 8*i + 8)
            {
                subcontacts = contacts.GetRange(8 * i, count - 8*i);
            }
            else
            {
                subcontacts = contacts.GetRange(8 * i, 8);
            }
            ListContact.PrintContacts(subcontacts);
            Console.Write("Select contact ID above, 0 to view next page, or 9 to return to main menu...");
            bool run = true;
            while (run)
            {
                ConsoleKey input = Console.ReadKey().Key;
                int selected = -1;
                if (input == keys[0] | input == keys[10])
                {
                    run = false;
                }
                else if (input == keys[9] | input == keys[19])
                {
                    return;
                }
                else
                {
                    selected = keys.IndexOf(input);
                    if (selected > 10) { selected -= 10; }
                }
                int upper = (count < 8 * i + 8) ? count - 8 * i : 8;
                if (selected > 0 & selected < upper)
                {
                    ViewContact.Menu(subcontacts[selected-1]);
                    return;
                }
            }
        }
    }
    public static void Recent()
    {
        List<Contact> contacts = Controller.GetContacts().OrderByDescending(x => x.LastAccess).ToList();
        int count = contacts.Count;
        List<Contact> subcontacts = contacts.GetRange(0, count < 8 ? count : 8);
        ListContact.PrintContacts(subcontacts);
        Console.Write("Select contact ID above or 9 to return to main menu...");
        bool run = true;
        while (run)
        {
            ConsoleKey input = Console.ReadKey().Key;
            int selected = -1;
            if (input == keys[9] | input == keys[19])
            {
                return;
            }
            else
            {
                selected = keys.IndexOf(input);
                if (selected > 10) { selected -= 10; }
            }
            int upper = count < 8 ? count : 8;
            if (selected > 0 & selected < upper)
            {
                ViewContact.Menu(subcontacts[selected - 1]);
                return;
            }
        }
    }
    public static void PrintContacts(List<Contact> contacts)
    {
        Console.Clear();
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
        }
        ConsoleTable.PrintTable(formatted);
    }
}
