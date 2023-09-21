using System.Reflection.Metadata.Ecma335;

namespace PhoneBook;

class Program
{
    public static void Main(string[] args)
    {
        var controller = new Controller();
        controller.ShowMenu();
    }
}
