namespace PhoneBook;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("PhoneBook starts. Please wait a moment...");
        using var phoneBookContext = new PhoneBookContext();
        var mainController = new MainController(phoneBookContext);
        mainController.ShowCategories();
    }
}
