namespace PhoneBook;

class ContactListView : BaseView
{
    private readonly ContactController controller;
    private readonly Category category;
    private readonly List<Contact> contacts;
    private int pointer;

    public ContactListView(ContactController controller, Category category, List<Contact> contacts)
    {
        this.controller = controller;
        this.category = category;
        this.contacts = contacts;
    }

    public override void Body()
    {
        Console.WriteLine($"Contacts in category '{category.Name}':");

        if (HasContacts())
        {
            WriteContactList();
        }
        else
        {
            Console.WriteLine("No contacts found.");
        }

        Console.WriteLine("---");
        if (HasContacts())
        {
            Console.WriteLine("Press arrow-up/-down to scroll through the list of contacts.");
            Console.WriteLine("Press arrow-left to change the category.");
            Console.WriteLine("Press arrow-right to view the details of the contact marked with '->'.");
        }
        Console.WriteLine("Press 'a' to add a new contact.");
        if (HasContacts())
        {
            Console.WriteLine("Press 'e' to edit the contact marked with '->'.");
            Console.WriteLine("Press 'd' to delete the contact marked with '->'.");
        }
        Console.WriteLine("Press 'x' to exit.");

        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.DownArrow:
                PointerDown();
                break;
            case ConsoleKey.UpArrow:
                PointerUp();
                break;
            case ConsoleKey.LeftArrow:
                controller.ChangeCategory();
                break;
            case ConsoleKey.RightArrow:
                Details();
                break;
            case ConsoleKey.A:
                controller.ShowAdd(category);
                break;
            case ConsoleKey.E:
                Edit();
                break;
            case ConsoleKey.D:
                Delete();
                break;
            case ConsoleKey.X:
                ContactController.ShowExit();
                break;
            default:
                Show();
                break;
        }
    }

    private bool HasContacts()
    {
        return contacts != null && contacts.Count > 0;
    }

    private void WriteContactList()
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

    private void PointerDown()
    {
        pointer++;
        if (HasContacts() && pointer > contacts.Count - 1)
        {
            pointer = contacts.Count - 1;
        }
        Show();
    }

    private void PointerUp()
    {
        pointer--;
        if (pointer < 0)
        {
            pointer = 0;
        }
        Show();
    }

    private void Details()
    {
        if (HasContacts())
        {
            controller.ShowDetails(contacts[pointer]);
        }
        else
        {
            Show();
        }
    }

    private void Edit()
    {
        if (HasContacts())
        {
            controller.ShowEdit(contacts[pointer]);
        }
        else
        {
            Show();
        }
    }

    private void Delete()
    {
        if (HasContacts())
        {
            controller.ShowDelete(contacts[pointer]);
        }
        else
        {
            Show();
        }
    }
}