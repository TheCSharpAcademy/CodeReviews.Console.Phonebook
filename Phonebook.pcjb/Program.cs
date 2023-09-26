namespace PhoneBook;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("PhoneBook starts. Please wait a moment...");
        using var phoneBookContext = new PhoneBookContext();
        var controller = new ContactController(phoneBookContext);
        controller.ShowList();
    }
}
