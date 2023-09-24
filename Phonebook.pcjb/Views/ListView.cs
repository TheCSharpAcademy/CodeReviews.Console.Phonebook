namespace PhoneBook;

using ConsoleTableExt;

class ListView : BaseView
{
    private readonly Controller controller;
    private readonly List<Contact> contacts;

    public ListView(Controller controller, List<Contact> contacts)
    {
        this.controller = controller;
        this.contacts = contacts;
    }

    public override void Body()
    {
        Console.WriteLine("All Contacts");
        if (contacts != null && contacts.Count > 0)
        {
            ConsoleTableBuilder.From(contacts).ExportAndWriteLine();
        }
        else
        {
            Console.WriteLine("No contacts found.");
        }
        Console.WriteLine("Press enter to return to menu.");
        Console.ReadLine();
        controller.ShowMenu();
    }
}