namespace PhoneBook;

class DetailView : BaseView
{
    private readonly Controller controller;
    private readonly Contact contact;

    public DetailView(Controller controller, Contact contact)
    {
        this.controller = controller;
        this.contact = contact;
    }

    public override void Body()
    {
        Console.WriteLine("Contact Details");
        WriteLineIfNotEmpty("Name", contact.Name);
        WriteLineIfNotEmpty("Phone-Number", contact.PhoneNumber);
        WriteLineIfNotEmpty("Email", contact.Email);
        Console.WriteLine("---");
        Console.WriteLine("Press arrow-left to select a different contact.");
        Console.WriteLine("Press 'e' to edit this contact.");
        Console.WriteLine("Press 'd' to delete this contact.");
        Console.WriteLine("Press 'x' to exit.");
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.LeftArrow:
                controller.ShowList();
                break;
            case ConsoleKey.E:
                controller.ShowEdit(contact.ContactID);
                break;
            case ConsoleKey.D:
                controller.ShowDelete();
                break;
            case ConsoleKey.X:
                controller.ShowExit();
                break;
            default:
                Show();
                break;
        }
    }

    private void WriteLineIfNotEmpty(string label, string? text)
    {
        if (!String.IsNullOrEmpty(text))
        {
            Console.WriteLine($"{label}: {text}");
        }
    }
}