namespace PhoneBook;
internal class Helper
{
    internal static void waitUserToPressAnyKeyToContinue()
    {
        Console.WriteLine("Enter any key to continue");
        Console.ReadLine();
        Console.Clear();
    }
}
