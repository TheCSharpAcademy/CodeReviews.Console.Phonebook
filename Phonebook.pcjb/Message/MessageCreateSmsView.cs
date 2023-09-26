namespace PhoneBook;

using System.Text;

class MessageCreateSmsView : BaseView
{
    private readonly MessageController controller;
    private readonly Contact contact;

    public MessageCreateSmsView(MessageController controller, Contact contact)
    {
        this.controller = controller;
        this.contact = contact;
    }
    public override void Body()
    {
        var config = Configuration.GetInstance();
        string? text;

        Console.WriteLine($"Send SMS to '{contact.Name}'");
        Console.WriteLine($"Mobile Number: {contact.MobileNumber}");
        Console.WriteLine("Text [Press <ESC> to finish entering the text]: ");
        ConsoleKeyInfo keyInfo;
        var textStringBuilder = new StringBuilder();
        do
        {
            keyInfo = Console.ReadKey(true);
            if (keyInfo.Key.Equals(ConsoleKey.Enter))
            {
                textStringBuilder.Append(System.Environment.NewLine);
                Console.Write(System.Environment.NewLine);
            }
            else if (!keyInfo.Key.Equals(ConsoleKey.Escape))
            {
                textStringBuilder.Append(keyInfo.KeyChar);
                Console.Write(keyInfo.KeyChar);
            }
        } while (!keyInfo.Key.Equals(ConsoleKey.Escape));
        Console.WriteLine("---");
        text = textStringBuilder.ToString();

        Console.WriteLine("Send SMS? [y/n]");
        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.Y:
                Console.WriteLine("Sending SMS. Please wait...");
                controller.SendSms(contact, text);
                break;
            default:
                controller.ShowContactDetails(contact);
                break;
        }
    }
}