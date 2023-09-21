using System.Reflection.Metadata.Ecma335;

namespace PhoneBook;

class Program
{
    public static void Main(string[] args)
    {
        var config = new Configuration();
        var phoneBookContect = new PhoneBookContext(config.DatabaseConnectionString);
        var controller = new Controller(phoneBookContect);
        controller.ShowMenu();
    }
}
