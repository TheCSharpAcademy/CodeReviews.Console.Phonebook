global using Phonebook.tonyissa.Models;
using Phonebook.tonyissa.UI;

while (true)
{
    try
    {
        await MenuController.InitMenu();
        break;
    }
    catch (Exception ex)
    {
        Console.Write(ex.ToString());
        Console.WriteLine("\nUnhandled exception. Press any key to restart the program.");
        Console.ReadKey();
        continue;
    }
}