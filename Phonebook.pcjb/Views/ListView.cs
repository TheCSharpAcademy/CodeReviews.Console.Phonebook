namespace PhoneBook;

class ListView : BaseView
{
    private readonly Controller controller;
    private readonly List<Contact> contacts;
    private int pointer;

    public ListView(Controller controller, List<Contact> contacts)
    {
        this.controller = controller;
        this.contacts = contacts;
    }

    public override void Body()
    {

        if (contacts != null && contacts.Count > 0)
        {
            int itemsAfterPointer = contacts.Count - 1 - pointer;
            int maxVisibleItemsBeforeAfter = 3;
            int first = pointer - Math.Min(maxVisibleItemsBeforeAfter, pointer);
            int last = pointer + Math.Min(maxVisibleItemsBeforeAfter, itemsAfterPointer);
            if (pointer - first < maxVisibleItemsBeforeAfter)
            {
                last += Math.Min(maxVisibleItemsBeforeAfter - (pointer - first), itemsAfterPointer);
            }
            if (last - pointer < maxVisibleItemsBeforeAfter)
            {
                first -= Math.Min(maxVisibleItemsBeforeAfter - (last - pointer), pointer);
            }
            if (first < 0) first = 0;
            if (first >= contacts.Count) first = contacts.Count - 1;
            if (last < 0) last = 0;
            if (last >= contacts.Count) last = contacts.Count - 1;

            for (int i = first; i <= last; i++)
            {
                var pointerSymbol = (i == pointer) ? "->" : "  ";
                Console.WriteLine($"{pointerSymbol} {contacts[i].Name}");
            }
        }
        else
        {
            Console.WriteLine("No contacts found.");
        }

        Console.WriteLine("---");
        if (contacts != null && contacts.Count > 0)
        {
            Console.WriteLine("Press arrow-up/-down to scroll through the list of contacts.");
            Console.WriteLine("Press arrow-right to view the details of the contact marked with '->'.");
        }
        Console.WriteLine("Press 'a' to add a new contact.");
        if (contacts != null && contacts.Count > 0)
        {
            Console.WriteLine("Press 'e' to edit the contact marked with '->'.");
            Console.WriteLine("Press 'd' to delete the contact marked with '->'.");
        }
        Console.WriteLine("Press 'x' to exit.");

        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.DownArrow:
                pointer++;
                if (contacts != null && pointer > contacts.Count - 1)
                {
                    pointer = contacts.Count - 1;
                }
                Show();
                break;
            case ConsoleKey.UpArrow:
                pointer--;
                if (pointer < 0)
                {
                    pointer = 0;
                }
                Show();
                break;
            case ConsoleKey.RightArrow:
                if (contacts != null && contacts.Count > 0)
                {
                    controller.ShowDetails(contacts[pointer].ContactID);
                }
                else
                {
                    Show();
                }
                break;
            case ConsoleKey.A:
                controller.ShowAdd();
                break;
            case ConsoleKey.E:
                if (contacts != null && contacts.Count > 0)
                {
                    controller.ShowEdit(contacts[pointer].ContactID);
                }
                else
                {
                    Show();
                }
                break;
            case ConsoleKey.D:
                if (contacts != null && contacts.Count > 0)
                {
                    controller.ShowDelete();
                }
                else
                {
                    Show();
                }
                break;
            case ConsoleKey.X:
                controller.ShowExit();
                break;
            default:
                Show();
                break;
        }
    }
}