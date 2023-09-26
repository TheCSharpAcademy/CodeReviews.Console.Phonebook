using System.Text;

namespace PhoneBook;

class MessageCreateMailView : BaseView
{
    private readonly MessageController controller;
    private readonly Contact contact;

    public MessageCreateMailView(MessageController controller, Contact contact)
    {
        this.controller = controller;
        this.contact = contact;
    }
    public override void Body()
    {
        var config = Configuration.GetInstance();
        string? subject;
        string? body;

        Console.WriteLine($"Send Email to '{contact.Name}'");
        Console.WriteLine($"From: {config.MailFrom}");
        Console.WriteLine($"To  : {contact.Email}");
        Console.Write("Subject: ");
        subject = Console.ReadLine();
        Console.WriteLine("Body [Press <ESC> to finish entering the body text]: ");
        ConsoleKeyInfo keyInfo;
        var bodyStringBuilder = new StringBuilder();
        do
        {
            keyInfo = Console.ReadKey(true);
            if (keyInfo.Key.Equals(ConsoleKey.Enter))
            {
                bodyStringBuilder.Append(System.Environment.NewLine);
                Console.Write(System.Environment.NewLine);
            }
            else if (!keyInfo.Key.Equals(ConsoleKey.Escape))
            {
                bodyStringBuilder.Append(keyInfo.KeyChar);
                Console.Write(keyInfo.KeyChar);
            }
        } while (!keyInfo.Key.Equals(ConsoleKey.Escape));
        Console.WriteLine("---");
        body = bodyStringBuilder.ToString();

        Console.WriteLine("Send email? [y/n]");
        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.Y:
                Console.WriteLine("Sending email. Please wait...");
                controller.SendMail(contact, subject, body);
                break;
            default:
                controller.ShowContactDetails(contact);
                break;
        }
    }
}