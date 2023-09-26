namespace PhoneBook;

class ContactDetailView : BaseView
{
    private readonly ContactController controller;
    private readonly Contact contact;

    public ContactDetailView(ContactController controller, Contact contact)
    {
        this.controller = controller;
        this.contact = contact;
    }

    public override void Body()
    {
        Console.WriteLine("Contact Details");
        WriteLineIfNotEmpty("Category", contact.Category.Name);
        WriteLineIfNotEmpty("Name", contact.Name);
        WriteLineIfNotEmpty("Phone Number", contact.PhoneNumber);
        WriteLineIfNotEmpty("Mobile Number", contact.MobileNumber);
        WriteLineIfNotEmpty("Email", contact.Email);
        Console.WriteLine("---");
        Console.WriteLine("Press arrow-left to select a different contact.");
        Console.WriteLine("Press 'e' to edit this contact.");
        Console.WriteLine("Press 'd' to delete this contact.");
        Console.WriteLine("Press 'm' to send an email to this contact.");
        Console.WriteLine("Press 'x' to exit.");
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.LeftArrow:
                controller.ShowList(contact.Category);
                break;
            case ConsoleKey.E:
                controller.ShowEdit(contact);
                break;
            case ConsoleKey.D:
                controller.ShowDelete(contact);
                break;
            case ConsoleKey.M:
                controller.SendMail(contact);
                break;
            case ConsoleKey.X:
                ContactController.ShowExit();
                break;
            default:
                Show();
                break;
        }
    }

    private static void WriteLineIfNotEmpty(string label, string? text)
    {
        if (!String.IsNullOrEmpty(text))
        {
            Console.WriteLine($"{label}: {text}");
        }
    }
}