using PhonebookLibrary.Controllers;
namespace PhonebookProgram;
internal class Program
{
    static void Main(string[] args)
    {
        Phonebook phonebook = new();
        phonebook.Menu();
    }
}

