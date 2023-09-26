namespace PhoneBook;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("PhoneBook starts. Please wait a moment...");
        using var phoneBookContext = new PhoneBookContext();
        var messageController = new MessageController();
        var contactController = new ContactController(phoneBookContext);
        contactController.SetMessageController(messageController);
        messageController.SetContactController(contactController);
        contactController.ShowList();
    }
}
