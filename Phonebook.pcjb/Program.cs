using System.Reflection.Metadata.Ecma335;

namespace PhoneBook;

class Program
{
    public static void Main(string[] args)
    {
        var phoneBookContext = new PhoneBookContext();
        var controller = new Controller(phoneBookContext);
        controller.ShowMenu();
    }
}
